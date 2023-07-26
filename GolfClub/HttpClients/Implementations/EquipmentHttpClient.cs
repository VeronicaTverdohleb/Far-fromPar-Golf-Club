using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace HttpClients.Implementations;

public class EquipmentHttpClient : IEquipmentService
{
    private readonly HttpClient client;

    public EquipmentHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ICollection<Equipment?>> getAllEquipmentAsync(string? name)
    {
        string query = ConstructQuery(name);
        HttpResponseMessage response = await client.GetAsync("/equipment" + query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Equipment?> equipments = JsonSerializer.Deserialize<ICollection<Equipment>>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return equipments;
    }

    private static string ConstructQuery(string? name)
    {
        string query = "";
        if (!string.IsNullOrEmpty(name))
        {
            query += $"?Name={name}";
        }


        if (!string.IsNullOrEmpty(name))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"namecontains={name}";
        }


        return query;
    }

    public async Task CreateEquipmentAsync(IEnumerable<EquipmentBasicDto> equipment, int amount)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Equipment", equipment);


        Console.WriteLine("in the http ");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task UpdateEquipmentAmount(string name, int amount)
    {
        string requestUrl = $"/Equipment/{name}/{amount}";
        HttpResponseMessage response = await client.DeleteAsync(requestUrl);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }


    public async Task DeleteEquipmentAsync(string? name)
    {
        HttpResponseMessage response = await client.DeleteAsync($"Equipment/{name}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task DeleteAllEquipmentByGameIdAsync(RentEquipmentDto dto)
    {
        string jsonDto = JsonSerializer.Serialize(dto);
        Console.WriteLine(jsonDto);
        StringContent body = new StringContent(jsonDto, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PatchAsync("/Equipment", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }


    public async Task RentEquipment(RentEquipmentDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/RentEquipment", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Equipment>?> GetAvailableEquipment()
    {
        var response = await client.GetAsync("/Equipments");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Equipment> result = JsonSerializer.Deserialize<ICollection<Equipment>>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return result;
    }


    public async Task<List<int>> GetGameEquipmentIds(int gameId)
    {
        var response = await client.GetAsync($"/RentEquipment/{gameId}");
        response.EnsureSuccessStatusCode();
        var responseStream = await response.Content.ReadAsStreamAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = await JsonSerializer.DeserializeAsync<List<int>>(responseStream, options);
        return result;
    }

    public async Task<ICollection<Equipment>?> GetEquipmentByGameIdAsync(int gameId)
    {
        var response = await client.GetAsync($"/Equipments/{gameId}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Equipment> result = JsonSerializer.Deserialize<ICollection<Equipment>>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return result;
    }
}