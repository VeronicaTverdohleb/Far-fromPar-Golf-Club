using Application.DaoInterfaces;
using Shared.Model;

namespace DataAccess.DAOs;

public class StatisticDao:IStatisticDao
{
    private readonly DataContext context;

    public StatisticDao(DataContext context)
    {
        this.context = context;
    }

    public Task<IEnumerable<Score>> GetAllScoresByPlayerAsync(string playerName)
    {
        IEnumerable<Score> scores = context.Scores.Where(s => s.PlayerUsername.Equals(playerName)&& s.Strokes!=0).OrderBy(s=>s.Id).AsEnumerable();
        return Task.FromResult(scores);
    }

    public Task<IEnumerable<Score>> GetAllScoresByTournamentAsync(string tournamentName)
    {
        IEnumerable<Score> scores = context.Tournaments.Where(t => t.Name.Equals(tournamentName))
            .SelectMany(t => t.Games)
            .SelectMany(g => g.Scores).Where(s => s.Strokes!=0).AsEnumerable();
        return Task.FromResult(scores);
    }
}