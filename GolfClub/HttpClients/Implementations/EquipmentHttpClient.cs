using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace HttpClients.Implementations;

public class EquipmentHttpClient: IEquipmentService
{
    private readonly HttpClient client;

    public EquipmentHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ICollection<Equipment>> getAllEquipmentAsync(string? name)
    {
        HttpResponseMessage response = await client.GetAsync("/Equipment");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Equipment> equipments = JsonSerializer.Deserialize<ICollection<Equipment>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return equipments; 
    }

    public async Task CreateEquipmentAsync(EquipmentBasicDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Equipment", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task UpdateEquipmentAmount(EquipmentBasicDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent amount = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/Equipment", amount);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<Equipment?> GetEquipmentByNameAsync(string name)
    {
        HttpResponseMessage response = await client.GetAsync($"/Equipment/{name}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Equipment equipment = JsonSerializer.Deserialize<Equipment>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return equipment;
    }

    public async Task DeleteEquipmentAsync(string name)
    {
        HttpResponseMessage response = await client.DeleteAsync($"Equipment/{name}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<int> CountOfEquipment(string name)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Equipment",name);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
             throw new Exception(content);
        } 
        int n= JsonSerializer.Deserialize<int>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return n; 
    }
}