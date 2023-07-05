using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EquipmentController: ControllerBase
{
    private readonly IEquipmentLogic equipmentLogic;


    public EquipmentController(IEquipmentLogic equipmentLogic)
    {
        this.equipmentLogic = equipmentLogic;
    }
    
     [HttpPost]
    public async Task<ActionResult<Equipment>> CreateEquipmentAsync(Equipment equipment)
    {
        try
        {
            Equipment equipment1 = await equipmentLogic.CreateEquipmentAsync(equipment);
            return Created($"/equipment/{equipment1.Id}", equipment1);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipmentAsync()
    {
        try
        {
            var equipments = await equipmentLogic.GetEquipmentAsync();
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

    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] EquipmentBasicDto dto)
    {
        try
        {
            await equipmentLogic.UpdateEquipmentAmount(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteEquipmentAsync([FromRoute] int id)
    {
        try
        {
            await equipmentLogic.DeleteEquipmentAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}