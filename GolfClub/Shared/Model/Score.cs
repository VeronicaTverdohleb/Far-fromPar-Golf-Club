namespace Shared.Model;

public class Score
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public ICollection<int> Strokes { get; set; } 
}