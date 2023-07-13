using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Model;

namespace Application.Logic;

public class StatisticLogic:IStatisticLogic
{
    private readonly IStatisticDao statisticDao;

    public StatisticLogic(IStatisticDao statisticDao)
    {
        this.statisticDao = statisticDao;
    }

    public Task<IEnumerable<Score>> GetAllScoresByPlayerAsync(string playerName)
    {
        return statisticDao.GetAllScoresByPlayerAsync(playerName);
    }
}