namespace Shared.Model;

public class Score
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    // Example {"1": "2"} - key = hole number, value = number of strokes
    public Dictionary<int, int> HolesAndStrokes { get; set; }
}