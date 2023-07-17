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
    
    /// <summary>
    /// Method used in Add Score use case
    /// The Member uses it to add their Score to a Game
    /// It returns Exception, if a User with dto.PlayerUsername is not found
    /// </summary>
    /// <param name="dto"></param>
    /// <exception cref="Exception"></exception>
    public async Task UpdateFromMemberAsync(ScoreBasicDto dto)
    {
        User? user = await userDao.GetByUsernameAsync(dto.PlayerUsername);
        if (user == null)
            throw new Exception($"User with username {dto.PlayerUsername} does not exist");
        
        // If any of the strokes in the list are either 0 or more than 23, just set it to 23
        for (int i = 0; i < 18; i++)
        {
            if (dto.Strokes[i] == 0 || dto.Strokes[i] > 23)
                dto.Strokes[i] = 23;
        }
        await scoreDao.UpdateFromMemberAsync(dto);
    }

    /// <summary>
    /// Method used in Manage Score use case 
    ///  The Employee uses it to manage Score in a Game
    /// It returns Exception, if a User with dto.PlayerUsername is not found
    /// </summary>
    /// <param name="dto"></param>
    public async Task UpdateFromEmployeeAsync(ScoreUpdateDto dto)
    {
        User? user = await userDao.GetByUsernameAsync(dto.PlayerUsername);
        if (user == null)
            throw new Exception($"User with username {dto.PlayerUsername} does not exist");

        await scoreDao.UpdateFromEmployeeAsync(dto);
    }
}