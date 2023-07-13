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
    /// As well as updates the Game that the Scores belong to
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

    public Task<Score?> GetScoreByUsernameAndHoleNumberAsync(string playerUsername, int holeNumber)
    {
        Score? existing = context.Scores.FirstOrDefault(score =>
            score.PlayerUsername == playerUsername && score.HoleNumber == holeNumber);
        return Task.FromResult(existing);
    }

    public async Task UpdateFromEmployeeAsync(ScoreUpdateDto dto)
    {
        
        foreach (int value in dto.HolesAndStrokes.Values)
        {
            
        }

    }
}