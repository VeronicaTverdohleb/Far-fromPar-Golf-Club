using Shared.Dtos.ScoreDto;
using Shared.Model;

namespace Application.LogicInterfaces;

/// <summary>
/// Interface implemented by ScoreLogic
/// </summary>
public interface IScoreLogic
{
    public Task UpdateFromMemberAsync(ScoreBasicDto dto);
    public Task UpdateFromEmployeeAsync(ScoreUpdateDto dto);
}