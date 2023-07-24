using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.LessonDto;
using Shared.Model;

namespace WebAPI.Controllers;
/// <summary>
/// Controller that handles lesson requests
/// It calls methods in LessonLogic
/// </summary>
[ApiController]
[Route("[controller]")]
public class LessonController : ControllerBase
{
    private readonly ILessonLogic lessonLogic;

    public LessonController(ILessonLogic lessonLogic)
    {
        this.lessonLogic = lessonLogic;
    }

    /// <summary>
    /// POST Endpoint for creating a Lesson
    /// Calls CreateAsync() in lessonLogic
    /// </summary>
    /// <param name="dto">Takes in a LessonCreationDto</param>
    /// <returns>Task<ActionResult<Lesson>></returns>
    [HttpPost]
    public async Task<ActionResult<Lesson>> CreateAsync([FromBody]LessonCreationDto dto)
    {
        try
        {
            Lesson lesson = await lessonLogic.CreateAsync(dto);
            return Created($"/Lesson/{lesson.Id}", lesson);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}