using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface ITournamentLogic
{
    public Task<Tournament> CreateTournamentAsync(CreateTournamentDto dto);
    public Task<Tournament> GetTournamentByNameAsync(string name);
    public Task DeleteTournamentAsync(string name);
    public Task<IEnumerable<Tournament>> GetAllTournamentsAsync();
    public Task<IEnumerable<User>> GetAllTournamentPlayersAsync(string name);
    public Task RegisterPlayerAsync(RegisterPlayerDto dto);
}