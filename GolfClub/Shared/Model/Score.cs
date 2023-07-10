namespace Shared.Model;

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