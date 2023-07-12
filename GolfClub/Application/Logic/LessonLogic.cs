using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos.LessonDto;
using Shared.Model;

namespace Application.Logic;

public class LessonLogic:ILessonLogic
{
    private readonly ILessonDao lessonDao;

    public LessonLogic(ILessonDao lessonDao)
    {
        this.lessonDao = lessonDao;
    }
    
    public async Task<Lesson> CreateAsync(LessonCreationDto dto)
    {
        Lesson todo = new Lesson(dto.DateAndTime, dto.Player, dto.Instructor);
        Lesson created = await lessonDao.CreateAsync(todo);
        return created;
    }
}