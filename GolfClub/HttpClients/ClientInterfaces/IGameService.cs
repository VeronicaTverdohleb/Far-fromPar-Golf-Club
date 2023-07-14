using Shared.Dtos.GameDto;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

/// <summary>
/// Interface implemented by GameHttpClient
/// </summary>
public interface IGameService
{
    public Task CreateAsync(GameBasicDto dto);
    public Task<Game?> GetActiveGameByUsernameAsync(string username);
    public Task<ICollection<Game>> GetAllGamesByUsernameAsync(string username);
    public Task<Game?> GetGameByIdAsync(int id);
}