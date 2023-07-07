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
        Tournament? existing = context.Tournaments.Include(tournament => tournament.Games).FirstOrDefault(tournament => tournament.Name.Equals(name));
        return Task.FromResult(existing);
    }

    public async Task DeleteTournamentAsync(string name)
    {
        Tournament? existing = await GetTournamentByNameAsync(name);
        if (existing==null)
        {
            throw new Exception($"Tournament with name {name} doesn't exist");
        }

        context.Tournaments.Remove(existing);
        await context.SaveChangesAsync();
    }

    public Task<IEnumerable<Tournament>> GetAllTournamentsAsync()
    {
        IEnumerable<Tournament> tournaments = context.Tournaments.AsEnumerable();
        return Task.FromResult(tournaments);
    }
}