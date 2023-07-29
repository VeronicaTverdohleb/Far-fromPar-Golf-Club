using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace DataAccess.DAOs;

/// <summary>
/// Data Access Object responsible for interacting with the database
/// </summary>
public class TournamentDao:ITournamentDao
{
    private readonly DataContext context;

    public TournamentDao(DataContext context)
    {
        this.context = context;
    }
    
    /// <summary>
    /// Method that creates a new tournament in the database
    /// </summary>
    /// <param name="dto">dto containing the necessary information to create a new tournament</param>
    /// <returns>The newly added tournament</returns>
    public async Task<Tournament> CreateTournamentAsync(CreateTournamentDto dto)
    {
        Tournament tournament = new Tournament(dto.Name, dto.StartDate, dto.EndDate);
        EntityEntry<Tournament> created = await context.Tournaments.AddAsync(tournament);
        await context.SaveChangesAsync();
        return created.Entity;
    }

    /// <summary>
    /// Method that gets a tournament by its name
    /// </summary>
    /// <param name="name">the name of the tournament</param>
    /// <returns>Tournament object corresponding with the given name</returns>
    public Task<Tournament?> GetTournamentByNameAsync(string name)
    {
        Tournament? existing = context.Tournaments.Include(tournament => tournament.Games).FirstOrDefault(tournament => tournament.Name.Equals(name));
        return Task.FromResult(existing);
    }

    /// <summary>
    /// Method that deletes a tournament with the given name
    /// </summary>
    /// <param name="name">Name of the tournament</param>
    /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// Method that gets all tournaments.
    /// </summary>
    /// <returns>An IEnumerable of all tournament objects in the database</returns>
    public Task<IEnumerable<Tournament>> GetAllTournamentsAsync()
    {
        IEnumerable<Tournament> tournaments = context.Tournaments.AsEnumerable();
        return Task.FromResult(tournaments);
    }

    /// <summary>
    /// Method that gets all players in a tournament
    /// </summary>
    /// <param name="name">Name of the tournament</param>
    /// <returns>An IEnumerable of User objects that are registered at the given tournament</returns>
    public Task<IEnumerable<User>> GetAllTournamentPlayersAsync(string name)
    {
        IEnumerable<User> players = context.Tournaments
            .Where(t => t.Name.Equals(name))
            .SelectMany(t => t.Players)
            .AsEnumerable();

        return Task.FromResult(players);
    }

    /// <summary>
    /// Method that adds a player to the list of players in a tournament
    /// </summary>
    /// <param name="dto">Dto containing the necessary information to add a player to a tournament</param>
    /// <exception cref="Exception"></exception>
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