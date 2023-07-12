using Shared.Dtos.LessonDto;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface ILessonLogic
{
    Task<Lesson> CreateAsync(LessonCreationDto dto);
}