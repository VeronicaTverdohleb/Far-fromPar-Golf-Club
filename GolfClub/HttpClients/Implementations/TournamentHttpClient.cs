using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace HttpClients.Implementations;

/// <summary>
/// Class responsible for making REST requests to the Web API
/// </summary>
public class TournamentHttpClient:ITournamentService
{
    private readonly HttpClient client;

    public TournamentHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    /// <summary>
    /// Method making a POST request in order to create a new tournament.
    /// </summary>
    /// <param name="dto">dto with the necessary information to create a tournament</param>
    /// <exception cref="Exception"></exception>
    public async Task CreateTournamentAsync(CreateTournamentDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Tournament", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    /// <summary>
    /// Method making a GET request to get a tournament by its name
    /// </summary>
    /// <param name="name">The name of the tournament</param>
    /// <returns>The tournament with the given name</returns>
    /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// Method making a DELETE request to remove a tournament by its name
    /// </summary>
    /// <param name="name">the name of the tournament</param>
    /// <exception cref="Exception"></exception>
    public async Task DeleteTournamentAsync(string name)
    {
        HttpResponseMessage response = await client.DeleteAsync($"/Tournament/{name}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    /// <summary>
    /// Method making a GET request requesting all of the tournaments.
    /// </summary>
    /// <returns>An ICollection of all Tournament objects</returns>
    /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// Method making a GET request requesting all the players that are registered to a given tournament
    /// </summary>
    /// <param name="name">name of the tournament</param>
    /// <returns>An ICollection of User objects that are registered to the tournament with the given name</returns>
    /// <exception cref="Exception"></exception>
    public async Task<ICollection<User>> GetAllTournamentPlayersAsync(string name)
    {
        HttpResponseMessage response = await client.GetAsync($"/Tournament/{name}/Players");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<User> players = JsonSerializer.Deserialize<ICollection<User>>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return players;
    }

    /// <summary>
    /// Method that makes a PATCH request in order to add a player to a tournament
    /// </summary>
    /// <param name="dto">dto containing the information needed to register a player to a given tournament</param>
    /// <exception cref="Exception"></exception>
    public async Task AddPlayerAsync(RegisterPlayerDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PatchAsync("/Tournament", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            if (content==null)
            {
                throw new Exception("content was null!");
            }
            throw new Exception(content);
        }
    }
}