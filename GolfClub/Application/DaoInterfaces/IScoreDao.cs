using Shared.Dtos.ScoreDto;
using Shared.Model;

namespace Application.DaoInterfaces;

/// <summary>
/// Interface implemented by ScoreDao
/// </summary>
public interface IScoreDao
{
    public Task UpdateFromMemberAsync(ScoreBasicDto score);
    public Task UpdateFromEmployeeAsync(ScoreUpdateDto dto);

}