using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Model;

namespace HttpClients.Implementations;

public class StatisticHttpClient:IStatisticService
{
    private readonly HttpClient client;

    public StatisticHttpClient(HttpClient client)
    {
        this.client = client;
    }

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
}