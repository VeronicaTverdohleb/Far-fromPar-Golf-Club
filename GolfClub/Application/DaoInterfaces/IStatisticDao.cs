using Shared.Model;

namespace Application.DaoInterfaces;
/// <summary>
/// Interface implemented by StatisticDao
/// </summary>
public interface IStatisticDao
{
    Task<IEnumerable<Score>> GetAllScoresByPlayerAsync(string playerName);
    Task<IEnumerable<Score>> GetAllScoresByTournamentAsync(string tournamentName);
}