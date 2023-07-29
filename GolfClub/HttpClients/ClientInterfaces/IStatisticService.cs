using Shared.Model;

namespace HttpClients.ClientInterfaces;

/// <summary>
/// Interface implemented by StatisticHttpClient
/// </summary>
public interface IStatisticService
{
   public Task<ICollection<Score>> GetAllScoresByPlayerAsync(string playerName);
   public Task<ICollection<Score>> GetAllScoresByTournamentAsync(string tournamentName);
}