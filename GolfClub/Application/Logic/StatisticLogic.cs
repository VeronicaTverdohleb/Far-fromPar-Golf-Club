using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Model;

namespace Application.Logic;
/// <summary>
/// Logic for async methods related to statistics
/// </summary>
public class StatisticLogic:IStatisticLogic
{
    private readonly IStatisticDao statisticDao;

    /// <summary>
    /// Constructor for StatisticLogic
    /// </summary>
    /// <param name="statisticDao">Instantiating the interface</param>
    public StatisticLogic(IStatisticDao statisticDao)
    {
        this.statisticDao = statisticDao;
    }

    /// <summary>
    /// Method that gets all scores by a player
    /// </summary>
    /// <param name="playerName">name of the player</param>
    /// <returns>An IEnumerable of Score objects by the chosen player</returns>
    public Task<IEnumerable<Score>> GetAllScoresByPlayerAsync(string playerName)
    {
        return statisticDao.GetAllScoresByPlayerAsync(playerName);
    }

    /// <summary>
    /// Method that gets all scores from a tournament
    /// </summary>
    /// <param name="tournamentName">the name of the tournament</param>
    /// <returns>an IEnumerable of Score objects that are part of the selected tournament</returns>
    public Task<IEnumerable<Score>> GetAllScoresByTournamentAsync(string tournamentName)
    {
        return statisticDao.GetAllScoresByTournamentAsync(tournamentName);
    }
}