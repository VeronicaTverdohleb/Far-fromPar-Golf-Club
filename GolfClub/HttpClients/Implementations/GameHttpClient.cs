using System.Net.Http.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos.GameDto;

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
}