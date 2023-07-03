using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Model;

namespace HttpClients.Implementations;

public class HoleHttpClient : IHoleService
{
    private readonly HttpClient client;
    private IHoleService holeService;

    public HoleHttpClient(HttpClient client)
    {
        this.client = client;
    }


    public async Task<ICollection<Hole>> GetAllHolesAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/Hole");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Hole> holes = JsonSerializer.Deserialize<ICollection<Hole>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return holes;
    }
}