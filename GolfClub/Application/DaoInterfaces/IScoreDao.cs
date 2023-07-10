using Shared.Dtos.ScoreDto;
using Shared.Model;

namespace Application.DaoInterfaces;

public interface IScoreDao
{
    public Task<Score> CreateAsync(ScoreBasicDto score);
}