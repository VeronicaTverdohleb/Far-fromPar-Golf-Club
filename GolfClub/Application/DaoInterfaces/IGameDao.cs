using Shared.Model;

namespace Application.DaoInterfaces;

public interface IGameDao
{
    public Task<Game> CreateAsync(Game game);
    public Task<IEnumerable<Game>> GetGamesByUser(User user);
}