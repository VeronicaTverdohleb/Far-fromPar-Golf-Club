using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace WebAPI.Controllers;

/// <summary>
/// Controller definition for Equipment-related requests, it communicates on request-reply basis
/// with EquipmentHttpClient in the BlazorApp
/// It calls methods in the EquipmentLogic
/// </summary>
[ApiController]
[Route("[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentLogic equipmentLogic;


    public EquipmentController(IEquipmentLogic equipmentLogic)
    {
        this.equipmentLogic = equipmentLogic;
    }

    /// <summary>
    /// POST endpoint that accepts requests with equipment creation info
    /// calls the "CreateEquipmentAsync" from the logic
    /// </summary>
    /// <param name="equipment"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    [HttpPost("/Equipment")]
    public async Task<ActionResult<IEnumerable<Equipment>>> CreateEquipmentAsync(
        IEnumerable<EquipmentBasicDto> equipment, int amount)
    {
        try
        {
            IEnumerable<Equipment> equipmentToCreate = await equipmentLogic.CreateEquipmentAsync(equipment, amount);
            return Created("/equipments", equipmentToCreate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// POST endpoint that accepts requests with renting equipment info
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("RentEquipment")]
    public async Task<ActionResult<Game>> RentEquipmentAsync([FromBody] RentEquipmentDto dto)
    {
        try
        {
            Console.WriteLine("in controller");
            await equipmentLogic.RentEquipment(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// GET endpoint that calls a method in the logic and returns equipments based on name query
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipmentAsync([FromQuery] string? name)
    {
        try
        {
            SearchEquipmentDto parameter = new SearchEquipmentDto(name);
            var equipments = await equipmentLogic.GetEquipmentAsync(parameter);
            return Ok(equipments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// GET endpoint that calls a method in the logic and returns an equipment based on id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Equipment>> GetEquipmentById([FromRoute] int id)
    {
        try
        {
            Equipment result = (await equipmentLogic.GetEquipmentByIdAsync(id))!;
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


    /// <summary>
    /// GET endpoint that calls a method in the logic and returns equipment based on name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("{name:required}")]
    public async Task<ActionResult<Equipment>> GetEquipmentByName([FromQuery] string name)
    {
        try
        {
            Equipment result = (await equipmentLogic.GetEquipmentByNameAsync(name))!;
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// GEt endpoint that calls a method from the logic and returns a list of equipments not associated with a game
    /// </summary>
    /// <returns></returns>
    [HttpGet("/Equipments")]
    public async Task<ActionResult<IEnumerable<Equipment>?>> GetAvailableEquipmentAsync()
    {
        try
        {
            var availableEquipment = await equipmentLogic.GetAvailableEquipmentAsync();

            return Ok(availableEquipment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// GET endpoint which calls a method from the logic and returns equipments associated with a game by its id
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
    [HttpGet, Route("/Equipments/{gameId:int}")]
    public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipmentByGameIdAsync([FromRoute] int gameId)
    {
        Console.WriteLine(gameId);
        try
        {
            var equipment = await equipmentLogic.GetEquipmentByGameIdAsync(gameId);
            return Ok(equipment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// DELETE endpoint which calls a method from the logic and deletes
    /// the amount of equipment by name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    [HttpDelete("{name:required}/{amount:int}")]
    public async Task<ActionResult> UpdateAsync(string name, int amount)
    {
        try
        {
            await equipmentLogic.UpdateEquipmentAsync(name, amount);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// DELETE endpoint which calls a method from the logic and deletes all the equipment by the name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpDelete("{name:required}")]
    public async Task<ActionResult> DeleteEquipmentAsync([FromRoute] string name)
    {
        try
        {
            await equipmentLogic.DeleteEquipmentAsync(name);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// PATCH endpoint which calls a method from the logic which updates the equipments associated with a game
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPatch]
    public async Task<ActionResult> DeleteAllEquipmentByGameIdAsync([FromBody] RentEquipmentDto dto)
    {
        Console.WriteLine("In the controller");
        try
        {
            await equipmentLogic.DeleteAllEquipmentByGameIdAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}