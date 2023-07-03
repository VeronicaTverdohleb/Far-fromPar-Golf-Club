using Shared.Model;

namespace Shared.Dtos.ScoreDto;

public class ScoreBasicDto
{
    public string PlayerUsername { get; set; }   // Basically a user Id
    public Hole Hole { get; set; }
    public int Strokes { get; set; }

    public ScoreBasicDto(string playerUsername, Hole hole, int strokes)
    {
        PlayerUsername = playerUsername;
        Hole = hole;
        Strokes = strokes;
    }
}