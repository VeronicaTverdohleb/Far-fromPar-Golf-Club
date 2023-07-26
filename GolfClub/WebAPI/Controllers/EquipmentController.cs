using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentLogic equipmentLogic;


    public EquipmentController(IEquipmentLogic equipmentLogic)
    {
        this.equipmentLogic = equipmentLogic;
    }

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