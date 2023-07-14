using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos.GameDto;
using Shared.Model;

namespace HttpClients.Implementations;

/// <summary>
/// Class responsible for making REST requests towards Web API
/// </summary>
public class GameHttpClient : IGameService
{
    private readonly HttpClient client;

    public GameHttpClient(HttpClient client)
    {
        this.client = client;
    }

    /// <summary>
    /// POST HTTP Request to create a Game
    /// </summary>
    /// <param name="dto"></param>
    /// <exception cref="Exception"></exception>
    public async Task CreateAsync(GameBasicDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Game", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    /// <summary>
    /// GET HTTP Request to get an active Game by username (Game with Score that has values 0)
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Game?> GetActiveGameByUsernameAsync(string username)
    {
        HttpResponseMessage response = await client.GetAsync($"/Game/{username}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Game? game = JsonSerializer.Deserialize<Game>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return game;
    }

    /// <summary>
    /// GET HTTP Request that fetches all Games by username
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ICollection<Game>> GetAllGamesByUsernameAsync(string username)
    {
        HttpResponseMessage response = await client.GetAsync($"/Games/{username}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Game> games = JsonSerializer.Deserialize<ICollection<Game>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return games;
    }

    /// <summary>
    /// GET HTTP Request 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Game?> GetGameByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/Game/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Game? game = JsonSerializer.Deserialize<Game>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return game;
        
    }
}