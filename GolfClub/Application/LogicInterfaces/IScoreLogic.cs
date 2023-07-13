using Shared.Dtos.ScoreDto;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IScoreLogic
{
    public Task UpdateFromMemberAsync(ScoreBasicDto dto);
    public Task UpdateFromEmployeeAsync(ScoreUpdateDto dto);
}