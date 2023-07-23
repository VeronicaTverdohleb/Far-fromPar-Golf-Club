using Shared.Dtos.LessonDto;
using Shared.Model;

namespace Application.DaoInterfaces;

public interface ILessonDao
{
    public Task<Lesson> CreateAsync(LessonCreationDto lesson, User user);
}