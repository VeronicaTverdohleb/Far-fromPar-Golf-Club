using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Model;

namespace WebAPI.Controllers;

/// <summary>
/// Web API method definition related to Hole functionality
/// Is responsible for receiving REST requests from IHoleService and calling methods
/// In the Application Logic
/// </summary>
[ApiController]
[Route("[controller]")]
public class HoleController : ControllerBase
{
    private readonly IHoleLogic holeLogic;

    public HoleController(IHoleLogic holeLogic)
    {
        this.holeLogic = holeLogic;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Hole>>> GetAsync()
    {
        try
        {
            var ingredients = await holeLogic.GetAsync();
            return Ok(ingredients);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    
    


}