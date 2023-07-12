using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.LessonDto;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LessonController : ControllerBase
{
    private readonly ILessonLogic lessonLogic;

    public LessonController(ILessonLogic lessonLogic)
    {
        this.lessonLogic = lessonLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Lesson>> CreateAsync(LessonCreationDto dto)
    {
        try
        {
            Lesson lesson = await lessonLogic.CreateAsync(dto);
            return Created($"/lessons/{lesson.Id}", lesson);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}