using Shared.Dtos.LessonDto;
using Shared.Model;

namespace Application.DaoInterfaces;
/// <summary>
/// Interface implemented by LessonDao
/// </summary>
public interface ILessonDao
{
    public Task<Lesson> CreateAsync(LessonCreationDto lesson, User user);
}