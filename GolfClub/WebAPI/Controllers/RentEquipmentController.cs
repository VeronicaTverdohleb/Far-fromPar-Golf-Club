using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class RentEquipmentController: ControllerBase
{
    private readonly IEquipmentLogic equipmentLogic;
    //private readonly IGameLogic gameLogic;

    public RentEquipmentController(IEquipmentLogic equipmentLogic)//, IGameLogic gameLogic)
    {
        this.equipmentLogic = equipmentLogic;
        //this.gameLogic = gameLogic;
    }
    
    [HttpPost]
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
    
   /* [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipment>>> GetAvailableEquipment()
    {
        try
        {
            var availableEquipment = equipmentLogic.GetAvailableEquipment();
            
            return Ok(availableEquipment); 
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
       
    }*/
    
}