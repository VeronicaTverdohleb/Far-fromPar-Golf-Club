using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

/// <summary>
/// Interface implemented by TournamentHttpClient
/// </summary>
public interface ITournamentService
{
    public Task CreateTournamentAsync(CreateTournamentDto dto);
    public Task<Tournament> GetTournamentByNameAsync(string name);
    public Task DeleteTournamentAsync(string name);
    public Task<ICollection<Tournament>> GetAllTournamentsAsync();
    public Task<ICollection<User>> GetAllTournamentPlayersAsync(string name);
    public Task AddPlayerAsync(RegisterPlayerDto dto);
}