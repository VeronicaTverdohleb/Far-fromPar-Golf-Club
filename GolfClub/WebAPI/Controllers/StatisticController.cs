using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Model;

namespace WebAPI.Controllers;

//API controller for requests regarding statistics.
[ApiController]
[Route("[controller]")]
public class StatisticController:ControllerBase
{
    private readonly IStatisticLogic statisticLogic;

    public StatisticController(IStatisticLogic statisticLogic)
    {
        this.statisticLogic = statisticLogic;
    }

    /// <summary>
    /// GET Method that gets all scores by a given player
    /// </summary>
    /// <param name="playerName">the name of the player</param>
    /// <returns>An IEnumerable of Score objects from the given player</returns>
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
    
    /// <summary>
    /// GET method that gets all scores from a given tournament.
    /// </summary>
    /// <param name="tournamentName">the name of the tournament</param>
    /// <returns>An IEnumerable of Score objects that are linked to the tournament</returns>
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