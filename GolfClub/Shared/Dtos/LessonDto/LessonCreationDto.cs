using Shared.Model;

namespace Shared.Dtos.LessonDto;
/// <summary>
/// Data Transfer Object used in Lesson-related use cases
/// </summary>
public class LessonCreationDto
{
    public string Date { get; set; }
    public string Time { get; set; }
    public string PlayerUsername { get; set; }
    public string Instructor { get; set; }

    public LessonCreationDto(string date, string time, string playerUsername, string instructor)
    {
        Date = date;
        Time = time;
        PlayerUsername = playerUsername;
        Instructor = instructor;
    }
}