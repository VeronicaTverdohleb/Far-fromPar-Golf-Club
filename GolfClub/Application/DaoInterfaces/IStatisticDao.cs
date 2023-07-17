using Shared.Model;

namespace Application.DaoInterfaces;

public interface IStatisticDao
{
    Task<IEnumerable<Score>> GetAllScoresByPlayerAsync(string playerName);
    Task<IEnumerable<Score>> GetAllScoresByTournamentAsync(string tournamentName);
}