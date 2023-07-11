using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos.GameDto;
using Shared.Model;

namespace HttpClients.Implementations;

public class GameHttpClient : IGameService
{
    private readonly HttpClient client;
    private IGameService gameService;

    public GameHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(GameBasicDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Game", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<Game>? GetActiveGameByUsernameAsync(string username)
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
}