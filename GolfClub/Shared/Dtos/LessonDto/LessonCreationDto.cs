using Shared.Model;

namespace Shared.Dtos.LessonDto;

public class LessonCreationDto
{
    public DateTime DateAndTime { get; set; }
    public User Player { get; set; }
    public string Instructor { get; set; }
}