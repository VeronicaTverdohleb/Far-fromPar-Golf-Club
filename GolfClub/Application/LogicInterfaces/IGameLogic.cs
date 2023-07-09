using Shared.Dtos.GameDto;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IGameLogic
{
    public Task<Game> CreateAsync(GameBasicDto dto);
    public Task<Game?> GetActiveGameByUsernameAsync(string username);
}