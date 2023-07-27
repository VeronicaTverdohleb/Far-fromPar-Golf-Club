using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Model;

namespace WebAPI.Controllers;
/// <summary>
/// Controller that handles user requests
/// It calls methods in UserLogic
/// </summary>
[ApiController]
[Route(("[controller]"))]
public class UserController : ControllerBase
{
    private readonly IUserLogic userLogic;

    /// <summary>
    /// Instantiates UserLogic
    /// </summary>
    /// <param name="userLogic">IUserLogic</param>
    public UserController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }
    /// <summary>
    /// Method to take in post requests to Create a User
    /// </summary>
    /// <param name="dto">UserCreationDto</param>
    /// <returns>User</returns>
    [HttpPost("/User")]
    public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)
    {
        try
        {
            User newUser = await userLogic.CreateAsync(dto);
            return Created($"/User/{newUser.UserName}", newUser);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    /// <summary>
    /// Method to get all users that are members
    /// </summary>
    /// <returns>List of Users</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
    {
        try
        {
            var users = await userLogic.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    /// <summary>
    /// Method to Get a user by the Username
    /// </summary>
    /// <param name="userName">string</param>
    /// <returns>User</returns>
    [HttpGet("{userName:required}")]
    public async Task<ActionResult<User>> GetByUsernameAsync([FromRoute] string userName)
    {
        try
        {
            User result = (await userLogic.GetByUsernameAsync(userName))!;
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    /// <summary>
    /// Method to Delete a user by Username
    /// </summary>
    /// <param name="userName">string</param>
    /// <returns>Task ActionResult</returns>
    [HttpDelete("/User/{userName}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] string userName)
    {
        try
        {
            await userLogic.DeleteAsync(userName);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}