using System.Net.Http.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos.LessonDto;

namespace HttpClients.Implementations;

public class LessonHttpClient : ILessonService
{
    private readonly HttpClient client;

    public LessonHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(LessonCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/lesson", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}