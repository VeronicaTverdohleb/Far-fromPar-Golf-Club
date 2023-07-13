using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.GameDto;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameLogic gameLogic;


    public GameController(IGameLogic gameLogic)
    {
        this.gameLogic = gameLogic;
    }
    
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