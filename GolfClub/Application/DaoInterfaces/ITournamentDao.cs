using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace Application.DaoInterfaces;

public interface ITournamentDao
{
    Task<Tournament> CreateTournamentAsync(Tournament tournament);
    Task<Tournament?> GetTournamentByNameAsync(string name);

}