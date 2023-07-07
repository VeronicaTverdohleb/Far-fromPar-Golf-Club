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
        foreach (User player in dto.Players)
        {
            IEnumerable<Game> games = await gameDao.GetGamesByUser(player);
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

        Game newGame = new Game(dto.Scores, dto.Equipments, dto.Players);
        Game created = await gameDao.CreateAsync(newGame);
        return created;
    }
}