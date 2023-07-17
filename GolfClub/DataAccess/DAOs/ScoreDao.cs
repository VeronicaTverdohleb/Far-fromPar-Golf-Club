using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
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
    /// Method that adds all Scores in the scorecard to the database
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public async Task UpdateFromMemberAsync(ScoreBasicDto score)
    {
        /*
         Functionality: 
         1. There are 18 scores in the ScoreBasicDto, 
            those Scores need to be updated in the DB (they currently have value Strokes = 0)
         2. The first Score is returned (Not really correct, but oh well)
         */
        for (int i = 1; i < score.Strokes.Count+1; i++)
        {
            // Retrieve the Score that we want to update from the DB
            Score? existing = context.Scores
                .FirstOrDefault(s => s.PlayerUsername == score.PlayerUsername && s.HoleNumber == i && s.Strokes == 0);
            // Assign the Strokes value to the value from ScoreBasicDto 
            existing!.Strokes = score.Strokes[i-1];
            // Update in the database
            context.Scores.Update(existing);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Method that takes Scores from dto.HolesAndStrokes and updates
    /// The corresponding Scores in the DB
    /// </summary>
    /// <param name="dto"></param>
    public async Task UpdateFromEmployeeAsync(ScoreUpdateDto dto)
    {
        Game? existing = context.Games
            .Include(game => game.Scores)
            .FirstOrDefault(game => game.Id == dto.GameId);

        foreach (Score score in existing!.Scores!)
        {
            if (score.PlayerUsername.Equals(dto.PlayerUsername))
            {
                foreach (int key in dto.HolesAndStrokes.Keys)
                {
                    if (key == score.HoleNumber)
                    {
                        score.Strokes = dto.HolesAndStrokes[key];
                        context.Scores.Update(score);
                        await context.SaveChangesAsync();
                        Console.WriteLine($"Updated Score to this - HoleNo {score.HoleNumber}: Strokes {score.Strokes}");
                    }
                }
            }
        }
    }
}