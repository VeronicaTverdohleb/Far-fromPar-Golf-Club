using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace Application.Logic;

public class TournamentLogic:ITournamentLogic
{
    private readonly ITournamentDao tournamentDao;

    public TournamentLogic(ITournamentDao tournamentDao)
    {
        this.tournamentDao = tournamentDao;
    }
    
    public async Task<Tournament> CreateTournamentAsync(CreateTournamentDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name))
        {
            throw new Exception("Enter a name for the tournament!");
        }

        if (dto.EndDate < DateOnly.FromDateTime(DateTime.Today))
        {
            throw new Exception("The tournament would already be over!");
        }

        if (dto.StartDate > dto.EndDate)
        {
            throw new Exception("Incorrect start and end dates!");
        }
        Tournament newTournament = new Tournament(dto.Name, dto.StartDate, dto.EndDate);
        Tournament created = await tournamentDao.CreateTournamentAsync(newTournament);
        return created;
    }

    public async Task<Tournament> GetTournamentByNameAsync(string name)
    {
        Tournament? tournament = await tournamentDao.GetTournamentByNameAsync(name);
        if (tournament == null)
        {
            throw new Exception($"Tournament with name {name} not found");
        }

        return tournament;
    }

    public async Task DeleteTournamentAsync(string name)
    {
        Tournament? tournament = await tournamentDao.GetTournamentByNameAsync(name);
        if (tournament == null)
        {
            throw new Exception($"Tournament with name {name} doesn't exist");
        }
        await tournamentDao.DeleteTournamentAsync(name);
    }

    public Task<IEnumerable<Tournament>> GetAllTournamentsAsync()
    {
        return tournamentDao.GetAllTournamentsAsync();
    }

    public Task<IEnumerable<User>> GetAllTournamentPlayersAsync(string name)
    {
        return tournamentDao.GetAllTournamentPlayersAsync(name);
    }

    public async Task RegisterPlayerAsync(RegisterPlayerDto dto)
    {
        IEnumerable<User> registeredPlayers = await tournamentDao.GetAllTournamentPlayersAsync(dto.TournamentName);
        foreach (var player in registeredPlayers)
        {
            if (player.Name.Equals(dto.PlayerName))
            {
                throw new Exception("This player is already registered!");
            }
        }

        await tournamentDao.RegisterPlayerAsync(dto);
    }
}