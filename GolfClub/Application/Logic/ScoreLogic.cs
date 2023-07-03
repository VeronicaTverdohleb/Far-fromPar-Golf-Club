using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos.ScoreDto;
using Shared.Model;

namespace Application.Logic;

/// <summary>
/// Logic for Async methods used in the Web API
/// </summary>
public class ScoreLogic : IScoreLogic
{
    private readonly IScoreDao scoreDao;
    private readonly IUserDao userDao;

    /// <summary>
    /// Constructor for the ScoreLogic
    /// </summary>
    /// <param name="scoreDao">Instantiating IScoreDao</param>
    /// <param name="userDao">Instantiating IUserDao</param>
    public ScoreLogic(IScoreDao scoreDao, IUserDao userDao)
    {
        this.scoreDao = scoreDao;
        this.userDao = userDao;
    }
    
    public async Task<Score> CreateAsync(ScoreBasicDto dto)
    {
        User? user = await userDao.GetByUsernameAsync(dto.PlayerUsername);
        if (user == null)
            throw new Exception($"User with username {dto.PlayerUsername} does not exist");
        if (dto.Strokes == 0 || dto.Strokes > 23)
            dto.Strokes = 23;

        Score score = new Score(dto.PlayerUsername, dto.Hole, dto.Strokes);
        Score created = await scoreDao.CreateAsync(score);
        return created;
    }
}