using Shared.Model;

namespace Application.LogicInterfaces;

/// <summary>
/// Interface implemented by StatisticLogic
/// </summary>
public interface IStatisticLogic
{
    Task<IEnumerable<Score>> GetAllScoresByPlayerAsync(string playerName);
    Task<IEnumerable<Score>> GetAllScoresByTournamentAsync(string tournamentName);
}