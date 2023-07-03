using Shared.Model;

namespace Application.DaoInterfaces;

public interface IScoreDao
{
    public Task<Score> CreateAsync(Score score);
}