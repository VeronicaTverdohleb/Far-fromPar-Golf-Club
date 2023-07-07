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
}