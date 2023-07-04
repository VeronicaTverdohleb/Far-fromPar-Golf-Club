using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Model;

namespace DataAccess.DAOs;

/// <summary>
/// Data Access Object responsible for interacting with the database
/// Used in ScoreLogic
/// </summary>
public class ScoreDao : IScoreDao
{
    private readonly DataContext context;

    public ScoreDao(DataContext context)
    {
        this.context = context;
    }
    
    /// <summary>
    /// Method that adds a new Score to the database
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public async Task<Score> CreateAsync(Score score)
    {
        EntityEntry<Score> added = await context.Scores.AddAsync(score);
        await context.SaveChangesAsync();
        return added.Entity;
    }
}