using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos.LessonDto;
using Shared.Model;

namespace DataAccess.DAOs;
/// <summary>
/// This class interacts with the database
/// </summary>
public class LessonDao:ILessonDao
{

    private readonly DataContext context;

    /// <summary>
    /// Instantiates DataContext
    /// </summary>
    /// <param name="context">DataContext</param>
    public LessonDao(DataContext context)
    {
        this.context = context;
    }
    
    /// <summary>
    /// This method adds a Lesson to the database
    /// </summary>
    /// <param name="dto">LessonCreationDto</param>
    /// <returns>Task<Lesson></returns>
    public async Task<Lesson> CreateAsync(LessonCreationDto dto, User user)
    {
        DateOnly dateOnly = DateOnly.ParseExact(dto.Date, "yyyy-mm-dd", null);
        Lesson lesson = new Lesson(dateOnly, dto.Time, user, dto.Instructor);
        
        EntityEntry<Lesson> added = await context.Lessons.AddAsync(lesson);
        await context.SaveChangesAsync();
        return added.Entity;
    }
}