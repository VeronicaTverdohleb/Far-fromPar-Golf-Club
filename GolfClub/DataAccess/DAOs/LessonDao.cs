using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Model;

namespace DataAccess.DAOs;

public class LessonDao:ILessonDao
{

    private readonly DataContext context;

    public LessonDao(DataContext context)
    {
        this.context = context;
    }
    
    public async Task<Lesson> CreateAsync(Lesson lesson)
    {
        EntityEntry<Lesson> added = await context.Lessons.AddAsync(lesson);
        await context.SaveChangesAsync();
        return added.Entity;
    }
}