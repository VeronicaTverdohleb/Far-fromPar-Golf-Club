using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos.LessonDto;
using Shared.Model;

namespace Application.Logic;
/// <summary>
/// This Class takes care of the business logic for LessonLogic
/// </summary>
public class LessonLogic:ILessonLogic
{
    private readonly ILessonDao lessonDao;
    private readonly IUserDao userDao;

    /// <summary>
    /// 2 Argument constructor
    /// </summary>
    /// <param name="lessonDao">ILessonDao</param>
    /// <param name="userDao">IUserDao</param>
    public LessonLogic(ILessonDao lessonDao, IUserDao userDao)
    {
        this.lessonDao = lessonDao;
        this.userDao = userDao;
    }
    /// <summary>
    /// Passes on the request to create a new lesson to the DataAccess
    /// </summary>
    /// <param name="dto">LessonCreationDto</param>
    /// <returns>Lesson</returns>
    /// <exception cref="Exception"></exception>
    public async Task<Lesson> CreateAsync(LessonCreationDto dto)
    {
        User? user = await userDao.GetByUsernameAsync(dto.PlayerUsername);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        Lesson created = await lessonDao.CreateAsync(dto, user);
        return created;
    }
}