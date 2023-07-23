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
    
    public async Task<Tournament> CreateTournamentAsync(CreateTournamentDto dto)
    {
        Tournament tournament = new Tournament(dto.Name, dto.StartDate, dto.EndDate);
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

    public Task<IEnumerable<User>> GetAllTournamentPlayersAsync(string name)
    {
        IEnumerable<User> players = context.Tournaments
            .Where(t => t.Name.Equals(name))
            .SelectMany(t => t.Players)
            .AsEnumerable();

        return Task.FromResult(players);
    }

    public async Task RegisterPlayerAsync(RegisterPlayerDto dto)
    {
        Tournament? existing = await context.Tournaments.FirstOrDefaultAsync(t => t.Name.Equals(dto.TournamentName));
        User? player = await context.Users.FirstOrDefaultAsync(u => u.Name.Equals(dto.PlayerName));
        if (existing == null)
        {
            throw new Exception("this tournament does not exist");
        }

        if (player==null)
        {
            throw new Exception("This player does not exist");
        }
        if (existing.Players==null)
        {
            Console.WriteLine("players was null");
            existing.Players = new List<User>();
        }
        existing.Players.Add(player);
        await context.SaveChangesAsync();
    }
}