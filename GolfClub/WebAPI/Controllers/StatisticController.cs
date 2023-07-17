using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StatisticController:ControllerBase
{
    private readonly IStatisticLogic statisticLogic;

    public StatisticController(IStatisticLogic statisticLogic)
    {
        this.statisticLogic = statisticLogic;
    }

    [HttpGet("/Statistic/{playerName}")]
    public async Task<ActionResult<IEnumerable<Score>>> GetAllScoresByPlayerAsync([FromRoute] string playerName)
    {
        try
        {
            var scores = await statisticLogic.GetAllScoresByPlayerAsync(playerName);
            return Ok(scores);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("/StatisticT/{tournamentName}")]
    public async Task<ActionResult<IEnumerable<Score>>> GetAllScoresByTournamentAsync([FromRoute] string tournamentName)
    {
        try
        {
            var scores = await statisticLogic.GetAllScoresByTournamentAsync(tournamentName);
            return Ok(scores);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}