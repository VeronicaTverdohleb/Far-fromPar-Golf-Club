using System.Net.Http.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos.LessonDto;

namespace HttpClients.Implementations;
/// <summary>
/// Class Responsible for making REST requests
/// </summary>
public class LessonHttpClient : ILessonService
{
    private readonly HttpClient client;

    /// <summary>
    /// Instantiates HttpClient
    /// </summary>
    /// <param name="client">HttpClient</param>
    public LessonHttpClient(HttpClient client)
    {
        this.client = client;
    }

    /// <summary>
    /// POST Request to create a Lesson
    /// </summary>
    /// <param name="dto">LessonCreationDto</param>
    /// <exception cref="Exception">If IsSuccessStatusCode is false, throw Exception</exception>
    public async Task CreateAsync(LessonCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Lesson", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}