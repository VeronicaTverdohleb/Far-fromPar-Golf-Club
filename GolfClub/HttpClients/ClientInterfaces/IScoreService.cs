using Shared.Dtos.ScoreDto;

namespace HttpClients.ClientInterfaces;

/// <summary>
/// Interface implemented by ScoreService
/// </summary>
public interface IScoreService
{
    public Task CreateAsync(ScoreBasicDto dto);
}