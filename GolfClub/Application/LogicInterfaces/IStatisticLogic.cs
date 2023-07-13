using Shared.Model;

namespace Application.LogicInterfaces;

public interface IStatisticLogic
{
    Task<IEnumerable<Score>> GetAllScoresByPlayerAsync(string playerName);
}