namespace Shared.Model;

/// Model Class used in Score-related use cases and for DB creation
public class Score
{
    public int Id { get; set; }
    public string PlayerUsername { get; set; }
    public int HoleNumber { get; set; }
    public int Strokes { get; set; }

    public Score(string playerUsername, int holeNumber, int strokes)
    {
        PlayerUsername = playerUsername;
        HoleNumber = holeNumber;
        Strokes = strokes;
    }
    
    private Score() { }
}