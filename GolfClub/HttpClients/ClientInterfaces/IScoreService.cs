using Shared.Dtos.ScoreDto;

namespace HttpClients.ClientInterfaces;

/// <summary>
/// Interface implemented by ScoreService
/// </summary>
public interface IScoreService
{
    public Task UpdateFromMemberAsync(ScoreBasicDto dto);
    public Task UpdateFromEmployeeAsync(ScoreUpdateDto dto);
}