namespace Shared.Model;

public class Score
{
    public int Id { get; set; }
    public int PlayerId { get; set; }   // Basically a user Id
    public Hole Hole { get; set; }
    public int Strokes { get; set; }

    private Score() { }
}