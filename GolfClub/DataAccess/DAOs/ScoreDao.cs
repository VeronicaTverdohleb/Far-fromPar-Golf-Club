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
    /// Method that adds all Scores in the scorecard to the database
    /// As well as updates the Game that the Scores belong to
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public async Task<Score> CreateAsync(ScoreBasicDto score)
    {
        /*
         Functionality: 
         1. There are 18 scores in the ScoreBasicDto, they are added to the database
         2. The Game that the Scores belong to is updated with the list of Scores
         3. The first Score is returned (Not really correct, but oh well)
         */
        EntityEntry<Score> added = null!;

        ICollection<Score> scoresToGame = new List<Score>();
        for (int i = 0; i < 18; i++)
        {
            Score newScore = new Score(score.PlayerUsername, i + 1, score.Strokes[i]);
            scoresToGame.Add(newScore);
            added = await context.Scores.AddAsync(newScore);
            await context.SaveChangesAsync();
        }
        
        // Updating the Game
        // First, pull out the game from the database
        Game? game = context.Games.FirstOrDefault(game => game.Id == score.GameId);
        
        // Then, add the scores to it
        game!.Scores = scoresToGame;

        // And then, update it in the database
        context.Games.Update(game!);
        await context.SaveChangesAsync();
        
        return added!.Entity;
    }
}