using Shared.Dtos.LessonDto;

namespace HttpClients.ClientInterfaces;

public interface ILessonService
{
    Task CreateAsync(LessonCreationDto dto);
}