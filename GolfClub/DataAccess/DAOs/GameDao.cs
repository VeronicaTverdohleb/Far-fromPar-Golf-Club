using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos.GameDto;
using Shared.Model;

namespace DataAccess.DAOs;

/// <summary>
/// Data Access Object responsible for interacting with the database
/// Used in GameDao
/// </summary>
public class GameDao : IGameDao
{
    private readonly DataContext context;


    public GameDao(DataContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Method that inserts a new Game into the database
    /// Functionality is described below
    /// </summary>
    /// <param name="game"></param>
    /// <returns></returns>
    public async Task<Game> CreateAsync(GameBasicDto game)
    {
        /*
         Functionality of the method:
         1. Fetch players based on the usernames in the GameBasicDto
         2. Add the new Game to database
         3. If TournamentName in GameDto, ddd the Game to a Tournament
         4. Return Task<Game>
         */
        ICollection<User> players = new List<User>();
        foreach (string playerUsername in game.PlayerUsernames)
        {
            User? user = context.Users.FirstOrDefault(user => user.UserName == playerUsername);
            players.Add(user!);
        }

        Game newGame = new Game(null, null, players);
        EntityEntry<Game> addedGame = await context.Games.AddAsync(newGame);
        await context.SaveChangesAsync();
        
        // If TournamentName in GameBasicDto is not null, add the new Game to the Tournament
        if (game.TournamentName != null || !game.TournamentName!.Equals(""))
        {
            Tournament? tournament = context.Tournaments.FirstOrDefault(t => t.Name == game.TournamentName);
            ICollection<Game> gamesToTourey = new List<Game>();
            gamesToTourey.Add(newGame);
            
            tournament!.Games = gamesToTourey;
            context.Tournaments.Update(tournament);
            await context.SaveChangesAsync();
        }
        
        return addedGame.Entity;
    }

    
    /// <summary>
    /// Method that fetches a Game based on a username
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public Task<IEnumerable<Game>> GetGamesByUsername(string username)
    {
        User? user = context.Users.FirstOrDefault(user => user.UserName == username);
        
        IEnumerable<Game> games = context.Games
            .Include(game => game.Scores)
            .Include(game => game.Equipments)
            .Include(game => game.Players)
            .Where(game => game.Players.Contains(user!))
            .AsEnumerable();
        
        return Task.FromResult(games);
    }
}