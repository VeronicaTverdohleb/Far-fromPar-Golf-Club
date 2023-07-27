namespace Shared.Model;

/// <summary>
/// Model Class used in Lesson-related use cases
/// </summary>
public class InstructorLesson
{
    public string Id { get; set; }
    public string InstructorName { get; set; }
    public string LessonDate { get; set; }
    public string LessonTime { get; set; }
}