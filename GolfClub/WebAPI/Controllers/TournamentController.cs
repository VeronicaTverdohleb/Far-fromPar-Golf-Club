using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TournamentController:ControllerBase
{
    private readonly ITournamentLogic tournamentLogic;

    public TournamentController(ITournamentLogic tournamentLogic)
    {
        this.tournamentLogic = tournamentLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Tournament>> CreateTournamentAsync([FromBody] CreateTournamentDto dto)
    {
        try
        {
            Tournament created = await tournamentLogic.CreateTournamentAsync(dto);
            return Created($"/Tournament/{created.Name}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet,Route("/Tournament/{name}")]
    public async Task<ActionResult<Tournament>> GetTournamentByNameAsync([FromRoute] string name)
    {
        Console.WriteLine(name);
        try
        {
            Tournament tournament = await tournamentLogic.GetTournamentByNameAsync(name);
            return Ok(tournament);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("/Tournament/{name}")]
    public async Task<ActionResult> DeleteTournamentAsync([FromRoute]string name)
    {
        try
        {
            await tournamentLogic.DeleteTournamentAsync(name);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("/Tournament")]
    public async Task<ActionResult<IEnumerable<Tournament>>> GetAllTournamentsAsync()
    {
        try
        {
            var tournaments = await tournamentLogic.GetAllTournamentsAsync();
            return Ok(tournaments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("/Tournament/{name}/Players")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllTournamentPlayersAsync([FromRoute]string name)
    {
        try
        {
            var players = await tournamentLogic.GetAllTournamentPlayersAsync(name);
            return Ok(players);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> RegisterPlayerAsync([FromBody] RegisterPlayerDto dto)
    {
        Console.WriteLine("why am i here");
        try
        {
            await tournamentLogic.RegisterPlayerAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}