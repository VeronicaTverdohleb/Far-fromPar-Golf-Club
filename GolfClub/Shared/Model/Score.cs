namespace Shared.Model;

public class Score
{
    public int Id { get; set; }
    public string PlayerUsername { get; set; }
    public Hole Hole { get; set; }
    public int Strokes { get; set; }

    public Score(string playerUsername, Hole hole, int strokes)
    {
        PlayerUsername = playerUsername;
        Hole = hole;
        Strokes = strokes;
    }
    
    private Score() { }
}