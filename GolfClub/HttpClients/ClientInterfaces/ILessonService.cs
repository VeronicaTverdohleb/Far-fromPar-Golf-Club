using Shared.Dtos.LessonDto;

namespace HttpClients.ClientInterfaces;
/// <summary>
/// Interface implemented by LessonHttpClient
/// </summary>
public interface ILessonService
{
    Task CreateAsync(LessonCreationDto dto);
}