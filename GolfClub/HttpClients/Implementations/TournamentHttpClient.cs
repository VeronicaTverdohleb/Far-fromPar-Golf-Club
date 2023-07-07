using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace HttpClients.Implementations;

public class TournamentHttpClient:ITournamentService
{
    private readonly HttpClient client;

    public TournamentHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task CreateTournamentAsync(CreateTournamentDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Tournament", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<Tournament> GetTournamentByNameAsync(string name)
    {
        Console.WriteLine(name);
        HttpResponseMessage response = await client.GetAsync($"/Tournament/{name}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Tournament tournament = JsonSerializer.Deserialize<Tournament>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return tournament;
    }

    public async Task DeleteTournamentAsync(string name)
    {
        HttpResponseMessage response = await client.DeleteAsync($"/Tournament/{name}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Tournament>> GetAllTournamentsAsync()
    {
        HttpResponseMessage response = await client.GetAsync($"/Tournament");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Tournament> tournaments = JsonSerializer.Deserialize<ICollection<Tournament>>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return tournaments;
    }
}