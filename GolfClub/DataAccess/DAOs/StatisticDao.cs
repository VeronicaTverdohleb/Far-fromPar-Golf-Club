using Application.DaoInterfaces;
using Shared.Model;

namespace DataAccess.DAOs;

/// <summary>
/// Data Access Object responsible for interacting with the database.
/// </summary>
public class StatisticDao:IStatisticDao
{
    private readonly DataContext context;

    public StatisticDao(DataContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Method that gets all scores by a given player from the database
    /// </summary>
    /// <param name="playerName">the chosen player name</param>
    /// <returns>an IEnumerable of all Score objects by the given player</returns>
    public Task<IEnumerable<Score>> GetAllScoresByPlayerAsync(string playerName)
    {
        IEnumerable<Score> scores = context.Scores.Where(s => s.PlayerUsername.Equals(playerName)&& s.Strokes!=0).OrderBy(s=>s.Id).AsEnumerable();
        return Task.FromResult(scores);
    }

    /// <summary>
    /// Method that gets all scores by a given tournament from the database
    /// </summary>
    /// <param name="tournamentName">the name of the chosen tournament</param>
    /// <returns>an IEnumerable of all Score objects by a given tournament</returns>
    public Task<IEnumerable<Score>> GetAllScoresByTournamentAsync(string tournamentName)
    {
        IEnumerable<Score> scores = context.Tournaments.Where(t => t.Name.Equals(tournamentName))
            .SelectMany(t => t.Games)
            .SelectMany(g => g.Scores).Where(s => s.Strokes!=0).AsEnumerable();
        return Task.FromResult(scores);
    }
}