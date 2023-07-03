using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface ITournamentLogic
{
    public Task<Tournament> CreateTournamentAsync(CreateTournamentDto dto);
    public Task<Tournament> GetTournamentByNameAsync(string name);
}