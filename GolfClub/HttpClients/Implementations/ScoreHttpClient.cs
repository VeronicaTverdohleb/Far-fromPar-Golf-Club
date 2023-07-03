using System.Net.Http.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos.ScoreDto;

namespace HttpClients.Implementations;

/// <summary>
/// Class responsible for making REST requests towards Web API
/// </summary>
public class ScoreHttpClient : IScoreService
{
    private readonly HttpClient client;
    private IScoreService scoreService;

    public ScoreHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    /// <summary>
    /// HTTP request to create a Score
    /// </summary>
    /// <param name="dto"></param>
    /// <exception cref="Exception"></exception>
    public async Task CreateAsync(ScoreBasicDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Score", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
    

    
    
}