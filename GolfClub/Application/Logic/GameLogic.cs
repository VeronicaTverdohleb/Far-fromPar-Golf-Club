using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos.GameDto;
using Shared.Model;

namespace Application.Logic;

public class GameLogic : IGameLogic
{
    private readonly IGameDao gameDao;

    public GameLogic(IGameDao gameDao)
    {
        this.gameDao = gameDao;
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
                    if (game.Scores != null)
                    {
                        foreach (Score score in game.Scores)
                        {
                            if (score.Strokes == 0)
                            {
                                throw new Exception($"User with username {score.PlayerUsername} has an unfinished game. Cannot create a new game.");
                            }
                        }
                    }
                }
            }
        }

        Game created = await gameDao.CreateAsync(dto);
        return created;
    }

    public Task<Game?> GetActiveGameByUsernameAsync(string username)
    {
        Task<IEnumerable<Game>> games = gameDao.GetGamesByUsername(username);
        Game gameToBeReturned = null;
        foreach (Game game in games.Result)
        {
            if (game.Scores == null || game.Scores.Count == 0)
            {
                gameToBeReturned = game;
                break;
            }
        }

        return Task.FromResult(gameToBeReturned);
    }
}