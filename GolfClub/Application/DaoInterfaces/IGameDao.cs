using Shared.Dtos.GameDto;
using Shared.Model;

namespace Application.DaoInterfaces;

public interface IGameDao
{
    public Task<Game> CreateAsync(GameBasicDto game);
    public Task<IEnumerable<Game>> GetGamesByUsername(string username);

    public Task<Game?> GetGameByIdAsync(int id);
}