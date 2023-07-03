using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.ScoreDto;
using Shared.Model;

namespace WebAPI.Controllers;

/// <summary>
/// Web API method definition related to Score functionality
/// Is responsible for receiving REST requests from IScoreService and calling methods
/// In the Application Logic
/// </summary>
[ApiController]
[Route("[controller]")]
public class ScoreController : ControllerBase
{
    private readonly IScoreLogic scoreLogic;

    public ScoreController(IScoreLogic scoreLogic)
    {
        this.scoreLogic = scoreLogic;
    }
    
    /// <summary>
    /// POST endpoint that accepts ScoreBasicDto as a parameter and calls the ScoreLogic
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Score>> CreateAsync([FromBody]ScoreBasicDto dto)
    {
        try
        {
            Score created = await scoreLogic.CreateAsync(dto);
            return Created($"/Score/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    

}