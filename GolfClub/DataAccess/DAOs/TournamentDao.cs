using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace DataAccess.DAOs;

public class TournamentDao:ITournamentDao
{
    private readonly DataContext context;

    public TournamentDao(DataContext context)
    {
        this.context = context;
    }
    
    public async Task<Tournament> CreateTournamentAsync(Tournament tournament)
    {
        EntityEntry<Tournament> created = await context.Tournaments.AddAsync(tournament);
        await context.SaveChangesAsync();
        return created.Entity;
    }

    public Task<Tournament?> GetTournamentByNameAsync(string name)
    {
        Tournament? existing = context.Tournaments.Include(tournament => tournament.Games)
            .ThenInclude(game => game.Players).FirstOrDefault(tournament => tournament.Name.Equals(name));
        return Task.FromResult(existing);
    }
}