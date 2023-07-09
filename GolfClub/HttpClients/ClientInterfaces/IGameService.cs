using Shared.Dtos.GameDto;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IGameService
{
    public Task CreateAsync(GameBasicDto dto);
    public Task<Game>? GetActiveGameByUsernameAsync(string username);
}