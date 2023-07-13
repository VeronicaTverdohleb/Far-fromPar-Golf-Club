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
        IEnumerable<Score> scores = context.Scores.Where(s => s.PlayerUsername.Equals(playerName)).OrderBy(s=>s.Id).AsEnumerable();
        return Task.FromResult(scores);
    }
}