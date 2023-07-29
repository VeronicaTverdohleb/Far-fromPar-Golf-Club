using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace Application.DaoInterfaces;
/// <summary>
/// Interface implemented by TournamentDao
/// </summary>
public interface ITournamentDao
{
    Task<Tournament> CreateTournamentAsync(CreateTournamentDto dto);
    Task<Tournament?> GetTournamentByNameAsync(string name);
    Task DeleteTournamentAsync(string name);
    Task<IEnumerable<Tournament>> GetAllTournamentsAsync();
    Task<IEnumerable<User>> GetAllTournamentPlayersAsync(string name);
    Task RegisterPlayerAsync(RegisterPlayerDto dto);

}