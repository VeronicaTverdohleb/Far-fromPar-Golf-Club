using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace WebAPI.Controllers;

/// <summary>
/// API Controller for requests regarding tournaments
/// </summary>
[ApiController]
[Route("[controller]")]
public class TournamentController:ControllerBase
{
    private readonly ITournamentLogic tournamentLogic;

    public TournamentController(ITournamentLogic tournamentLogic)
    {
        this.tournamentLogic = tournamentLogic;
    }

    /// <summary>
    /// POST Method that creates a new tournament
    /// </summary>
    /// <param name="dto">dto containing the necessary information to create a new tournament</param>
    /// <returns>The newly created tournament</returns>
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

    /// <summary>
    /// GET method that gets a tournament object by its name
    /// </summary>
    /// <param name="name">name of the tournament</param>
    /// <returns>Tournament object with the given name</returns>
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

    /// <summary>
    /// DELETE method that calls the TournamentLogic to delete the tournament with the given name
    /// </summary>
    /// <param name="name">name of the tournament</param>
    /// <returns></returns>
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

    /// <summary>
    /// GET Method that calls the TournamentLogic to get all tournament objects
    /// </summary>
    /// <returns>An IEnumerable of all Tournament objects</returns>
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

    /// <summary>
    /// GET Method that asks the TournamentLogic to get all players currently registered in the given tournament
    /// </summary>
    /// <param name="name">name of the tournament</param>
    /// <returns>IEnumerable of User objects that are registered to the given tournament.</returns>
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

    /// <summary>
    /// PATCH method that tells the TournamentLogic to register a player to a tournament
    /// </summary>
    /// <param name="dto">dto containing the necessary information to link a player to a tournament</param>
    /// <returns></returns>
    [HttpPatch]
    public async Task<ActionResult> RegisterPlayerAsync([FromBody] RegisterPlayerDto dto)
    {
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