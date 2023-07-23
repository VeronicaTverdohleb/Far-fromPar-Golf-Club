using Shared.Dtos.LessonDto;
using Shared.Model;

namespace Application.LogicInterfaces;
/// <summary>
/// Interface implemented for LessonLogic
/// </summary>
public interface ILessonLogic
{
    Task<Lesson> CreateAsync(LessonCreationDto dto);
}