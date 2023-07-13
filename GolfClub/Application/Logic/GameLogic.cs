using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos.GameDto;
using Shared.Model;

namespace Application.Logic;

public class GameLogic : IGameLogic
{
    private readonly IGameDao gameDao;
    private readonly IUserDao userDao;

    public GameLogic(IGameDao gameDao, IUserDao userDao)
    {
        this.gameDao = gameDao;
        this.userDao = userDao;
    }

    public async Task<Game> CreateAsync(GameBasicDto dto)
    {
        // If there is a player with unfinished game, the game will not be created and exception is thrown
        foreach (string playerUsername in dto.PlayerUsernames)
        {
            IEnumerable<Game> games = await gameDao.GetGamesByUsername(playerUsername);
            if (games.Any())
            {
                foreach (Game game in games.ToList())
                {
                    foreach (Score score in game.Scores!)
                    {
                        if (score.Strokes == 0)
                            throw new Exception($"User with username {playerUsername} has an unfinished game. Cannot create a new game with this user.");
                    }
                }
            }
        }

        Game created = await gameDao.CreateAsync(dto);
        return created;
    }

    // This method return a Game By Username that has a Score with Strokes = 0 
    // Each Game has 18 Scores per player, and if even a single Score has Strokes = 0
    // This method returns that Game
    public Task<Game?> GetActiveGameByUsernameAsync(string username)
    {
        User? user = userDao.GetByUsernameAsync(username).Result;
        if (user == null)
            throw new Exception("No user found");
        
        Task<IEnumerable<Game>> games = gameDao.GetGamesByUsername(username);
        Game gameToBeReturned = null;
        foreach (Game game in games.Result)
        {
            if (game.Scores == null || game.Scores.Count == 0)
            {
                gameToBeReturned = game;
                break;
            }
            foreach (Score score in game.Scores!)
            {
                if (score.PlayerUsername == username && score.Strokes == 0)
                {
                    gameToBeReturned = game;
                    break;
                }
            }
        }

        return Task.FromResult(gameToBeReturned);
    }

    public Task<IEnumerable<Game>> GetAllGamesByUsernameAsync(string username)
    {
        User? user = userDao.GetByUsernameAsync(username).Result;
        if (user == null)
            throw new Exception("No user found");
        return gameDao.GetGamesByUsername(username);
    }

    public async Task<Game?> GetGameByIdAsync(int id)
    {
        Game? game = await gameDao.GetGameByIdAsync(id);
        if (game == null)
        {
            throw new Exception($"Game with id {id} not found");
        }
        return game;
    }
}