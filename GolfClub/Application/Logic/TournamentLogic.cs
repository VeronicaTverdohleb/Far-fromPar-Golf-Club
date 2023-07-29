using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace Application.Logic;

public class TournamentLogic:ITournamentLogic
{
    private readonly ITournamentDao tournamentDao;

    /// <summary>
    /// Constructor for TournamentLogic
    /// </summary>
    /// <param name="tournamentDao">instantiating the interface</param>
    public TournamentLogic(ITournamentDao tournamentDao)
    {
        this.tournamentDao = tournamentDao;
    }
    
    /// <summary>
    /// Method with logic regarding the creation of a tournament
    /// </summary>
    /// <param name="dto">a dto containing the necessary information to create a tournament</param>
    /// <returns>The created tournament</returns>
    /// <exception cref="Exception"></exception>
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
        
        Tournament created = await tournamentDao.CreateTournamentAsync(dto);
        return created;
    }

    /// <summary>
    /// Method with logic regarding the retrieval of a tournament by its name
    /// </summary>
    /// <param name="name">the name of the tournament</param>
    /// <returns>The tournament with the given name</returns>
    /// <exception cref="Exception"></exception>
    public async Task<Tournament> GetTournamentByNameAsync(string name)
    {
        Tournament? tournament = await tournamentDao.GetTournamentByNameAsync(name);
        if (tournament == null)
        {
            throw new Exception($"Tournament with name {name} not found");
        }

        return tournament;
    }

    /// <summary>
    /// Method with logic regarding the deletion of a given tournament
    /// </summary>
    /// <param name="name">the name of the tournament to delete</param>
    /// <exception cref="Exception"></exception>
    public async Task DeleteTournamentAsync(string name)
    {
        Tournament? tournament = await tournamentDao.GetTournamentByNameAsync(name);
        if (tournament == null)
        {
            throw new Exception($"Tournament with name {name} doesn't exist");
        }
        await tournamentDao.DeleteTournamentAsync(name);
    }

    /// <summary>
    /// Method that gets all the existing tournaments
    /// </summary>
    /// <returns>an IEnumerable of all Tournament objects</returns>
    public Task<IEnumerable<Tournament>> GetAllTournamentsAsync()
    {
        return tournamentDao.GetAllTournamentsAsync();
    }

    /// <summary>
    /// Method that gets all players registered to a given tournament
    /// </summary>
    /// <param name="name">Name of the tournament</param>
    /// <returns>An IEnumerable of User objects that are registered to the given tournament</returns>
    public Task<IEnumerable<User>> GetAllTournamentPlayersAsync(string name)
    {
        return tournamentDao.GetAllTournamentPlayersAsync(name);
    }

    /// <summary>
    /// Method that registers a player to the current tournament
    /// </summary>
    /// <param name="dto">dto containing the information to register a player to a tournament</param>
    /// <exception cref="Exception"></exception>
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