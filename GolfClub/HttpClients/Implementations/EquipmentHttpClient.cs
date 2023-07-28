using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace HttpClients.Implementations;

/// <summary>
/// Class responsible for making REST requests towards Web API
/// </summary>
public class EquipmentHttpClient : IEquipmentService
{
    private readonly HttpClient client;

    public EquipmentHttpClient(HttpClient client)
    {
        this.client = client;
    }

    /// <summary>
    /// GET HTTP request to get all equipment by the name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ICollection<Equipment?>> GetAllEquipmentAsync(string? name)
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

    /// <summary>
    /// Method to construct a query for the "GetAllEquipmentAsync" method
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
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

    /// <summary>
    /// POST HTTP request to create a new equipment
    /// </summary>
    /// <param name="equipment"></param>
    /// <param name="amount"></param>
    /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// DELETE HTTP request to remove the amount of equipment specified based on the name 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="amount"></param>
    /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// DELETE HTTP request to delete all the equipments based on the name
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="Exception"></exception>
    public async Task DeleteEquipmentAsync(string? name)
    {
        HttpResponseMessage response = await client.DeleteAsync($"Equipment/{name}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    /// <summary>
    /// PATCH HTTP request to update the equipments associated to a game
    /// </summary>
    /// <param name="dto"></param>
    /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// POST HTTP request to associate equipments to a game
    /// </summary>
    /// <param name="dto"></param>
    /// <exception cref="Exception"></exception>
    public async Task RentEquipment(RentEquipmentDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("Equipment/RentEquipment", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    /// <summary>
    /// GET HTTP request to get all the available equipment
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
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


    /// <summary>
    /// GET HTTP request to get all equipments associated to a game by its id
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
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