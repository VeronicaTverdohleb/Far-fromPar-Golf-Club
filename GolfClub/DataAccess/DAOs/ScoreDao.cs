using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos.ScoreDto;
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
    public async Task<Score> CreateAsync(ScoreBasicDto score)
    {
        EntityEntry<Score> added = null!;
        for (int i = 0; i < 18; i++)
        {
            added = await context.Scores.AddAsync(new Score(score.PlayerUsername, i+1, score.Strokes[i]));
            await context.SaveChangesAsync();
        }
        return added!.Entity;
    }
}