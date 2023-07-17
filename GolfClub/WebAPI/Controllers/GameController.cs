using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.GameDto;
using Shared.Model;

namespace WebAPI.Controllers;

/// <summary>
/// Controller definition for Game-related requests, it communicates on request-reply basis
/// with GameHttpClient in the BlazorApp
/// It calls methods in the GameLogic
/// </summary>
[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameLogic gameLogic;


    public GameController(IGameLogic gameLogic)
    {
        this.gameLogic = gameLogic;
    }
    
    /// <summary>
    /// POST endpoint that accepts requests with Game creation info
    /// And calls CreateAsync() in the GameLogic
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Task<ActionResult<Game>></returns>
    [HttpPost]
    public async Task<ActionResult<Game>> CreateAsync([FromBody] GameBasicDto dto)
    {
        try
        {
            Game created = await gameLogic.CreateAsync(dto);
            return Created($"/Game/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    /// <summary>
    /// GET endpoint that calls a method in the GameLogic and returns
    /// an active Game by a username
    /// </summary>
    /// <param name="username"></param>
    /// <returns>Task<ActionResult<Game>></returns>
    [HttpGet,Route("/Game/{username}")]
    public async Task<ActionResult<Game>> GetActiveGameByUsernameAsync([FromRoute] string username)
    {
        try
        {
            Game game = (await gameLogic.GetActiveGameByUsernameAsync(username))!;
            return Ok(game);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// GET endpoint that calls a method in GameLogic and
    /// Returns a list of Games of a player
    /// </summary>
    /// <param name="username"></param>
    /// <returns>Task<ActionResult<IEnumerable<Game>>></returns>
    [HttpGet("/Games/{username}")]
    public async Task<ActionResult<IEnumerable<Game>>> GetAllGamesByUsernameAsync([FromRoute] string username)
    {
        try
        {
            var games = await gameLogic.GetAllGamesByUsernameAsync(username);
            return Ok(games);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// GET endpoint that calls method in GameLogic and
    /// Returns a Game by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Task<ActionResult<Game>></returns>
    [HttpGet,Route("/Game/{id:int}")]
    public async Task<ActionResult<Game>> GetGameByIdAsync([FromRoute] int id)
    {
        try
        {
            Game game = (await gameLogic.GetGameByIdAsync(id))!;
            return Ok(game);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
}