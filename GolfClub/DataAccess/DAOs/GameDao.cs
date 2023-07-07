using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Model;

namespace DataAccess.DAOs;

public class GameDao : IGameDao
{
    private readonly DataContext context;


    public GameDao(DataContext context)
    {
        this.context = context;
    }

    public async Task<Game> CreateAsync(Game game)
    {
        EntityEntry<Game> added = await context.Games.AddAsync(game);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public Task<IEnumerable<Game>> GetGamesByUser(User user)
    {
        IEnumerable<Game> games = context.Games
            .Include(game => game.Scores)
            .Include(game => game.Equipments)
            .Include(game => game.Players)
            .Where(game => game.Players.Contains(user))
            .AsEnumerable();
        
        return Task.FromResult(games);
       
        
    }
}