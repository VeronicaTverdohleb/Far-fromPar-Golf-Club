using Shared.Dtos.GameDto;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IGameLogic
{
    public Task<Game> CreateAsync(GameBasicDto dto);
    
    // This returns only Games that do not have a Score in it (considered unfinished) 
    // And that is always only 1 Game, as a Member can only have one finished Game at a time
    public Task<Game?> GetActiveGameByUsernameAsync(string username);   

    // This method returns all Games that a player has ever been part of
    public Task<IEnumerable<Game>> GetAllGamesByUsernameAsync(string username);
}