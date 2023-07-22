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
    private readonly IGameDao gameDao;

    /// <summary>
    /// Constructor for the ScoreLogic
    /// </summary>
    /// <param name="scoreDao">Instantiating IScoreDao</param>
    /// <param name="userDao">Instantiating IUserDao</param>
    /// <param name="gameDao">Instantiating IGameDao</param>
    public ScoreLogic(IScoreDao scoreDao, IUserDao userDao, IGameDao gameDao)
    {
        this.scoreDao = scoreDao;
        this.userDao = userDao;
        this.gameDao = gameDao;
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

        Game? game = await gameDao.GetGameByIdAsync(dto.GameId);
        if (game == null)
            throw new Exception($"Game with id {dto.GameId} does not exist");
        
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
        Game? game = await gameDao.GetGameByIdAsync(dto.GameId);
        if (game == null)
            throw new Exception($"Game with id {dto.GameId} does not exist");

        if (dto.HolesAndStrokes.Count > 18)
            throw new Exception("Too many scores! There are only 18 holes.");
        foreach (int holeNo in dto.HolesAndStrokes.Keys)
        {
            if (holeNo < 1 || holeNo > 18)
                throw new Exception($"The hole with Id {holeNo} does not exist");
        }
            
        await scoreDao.UpdateFromEmployeeAsync(dto);
    }
}