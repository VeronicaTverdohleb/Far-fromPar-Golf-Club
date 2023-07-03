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
}