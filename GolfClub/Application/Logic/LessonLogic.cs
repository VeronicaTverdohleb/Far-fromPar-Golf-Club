using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos.LessonDto;
using Shared.Model;

namespace Application.Logic;

public class LessonLogic:ILessonLogic
{
    private readonly ILessonDao lessonDao;
    private readonly IUserDao userDao;

    public LessonLogic(ILessonDao lessonDao, IUserDao userDao)
    {
        this.lessonDao = lessonDao;
        this.userDao = userDao;
    }
    
    public async Task<Lesson> CreateAsync(LessonCreationDto dto)
    {
        User? user = await userDao.GetByUsernameAsync(dto.PlayerUsername);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        DateOnly dateOnly = DateOnly.ParseExact(dto.Date, "yyyy-mm-dd", null);
        Lesson todo = new Lesson(dateOnly, dto.Time, user, dto.Instructor);
        Lesson created = await lessonDao.CreateAsync(todo);
        return created;
    }
}