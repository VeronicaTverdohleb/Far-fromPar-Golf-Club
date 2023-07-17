using Shared.Model;

namespace Application.DaoInterfaces;

public interface ILessonDao
{
    public Task<Lesson> CreateAsync(Lesson lesson);
}