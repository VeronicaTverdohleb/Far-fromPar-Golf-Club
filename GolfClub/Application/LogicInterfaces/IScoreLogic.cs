using Shared.Dtos.ScoreDto;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IScoreLogic
{
    public Task<Score> CreateAsync(ScoreBasicDto dto);
}