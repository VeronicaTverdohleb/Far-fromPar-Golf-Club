using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Model;

namespace HttpClients.Implementations;

/// <summary>
/// Class responsible for making REST requests to the Web API
/// </summary>
public class StatisticHttpClient:IStatisticService
{
    private readonly HttpClient client;

    public StatisticHttpClient(HttpClient client)
    {
        this.client = client;
    }

    /// <summary>
    /// Method that makes a GET request requesting all scores by a given player
    /// </summary>
    /// <param name="playerName">name of the player</param>
    /// <returns>An ICollection of all Score objects from a certain player</returns>
    /// <exception cref="Exception"></exception>
    public async Task<ICollection<Score>> GetAllScoresByPlayerAsync(string playerName)
    {
        HttpResponseMessage response = await client.GetAsync($"/Statistic/{playerName}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        
        ICollection<Score> scores = JsonSerializer.Deserialize<ICollection<Score>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return scores;
    }

    /// <summary>
    /// Method that makes a GET request that gets all the scores by a given tournament name
    /// </summary>
    /// <param name="tournamentName">the name of the tournament</param>
    /// <returns>An ICollection of Score objects from the given tournament</returns>
    /// <exception cref="Exception"></exception>
    public async Task<ICollection<Score>> GetAllScoresByTournamentAsync(string tournamentName)
    {
        HttpResponseMessage response = await client.GetAsync($"/StatisticT/{tournamentName}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        
        ICollection<Score> scores = JsonSerializer.Deserialize<ICollection<Score>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return scores;
    }
}