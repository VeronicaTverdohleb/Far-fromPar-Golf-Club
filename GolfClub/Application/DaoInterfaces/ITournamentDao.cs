using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace Application.DaoInterfaces;

public interface ITournamentDao
{
    Task<Tournament> CreateTournamentAsync(CreateTournamentDto dto);
    Task<Tournament?> GetTournamentByNameAsync(string name);
    Task DeleteTournamentAsync(string name);
    Task<IEnumerable<Tournament>> GetAllTournamentsAsync();
    Task<IEnumerable<User>> GetAllTournamentPlayersAsync(string name);
    Task RegisterPlayerAsync(RegisterPlayerDto dto);

}