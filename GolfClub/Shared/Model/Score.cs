namespace Shared.Model;

public class Score
{
    public int Id { get; set; }
    public int PlayerId { get; set; }   // Basically a user Id
    public int HoleNumber { get; set; }
    public int Strokes { get; set; } 
}