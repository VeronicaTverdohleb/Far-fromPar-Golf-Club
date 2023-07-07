using Shared.Dtos.GameDto;

namespace HttpClients.ClientInterfaces;

public interface IGameService
{
    public Task CreateAsync(GameBasicDto dto);
}