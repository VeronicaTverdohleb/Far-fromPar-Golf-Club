using Shared.Model;

namespace Shared.Dtos.ScoreDto;

public class ScoreBasicDto
{
    public string PlayerUsername { get; set; }   // Basically a user Id
    public int HoleNumber { get; set; }
    public int Strokes { get; set; }

    public ScoreBasicDto(string playerUsername, int holeNumber, int strokes)
    {
        PlayerUsername = playerUsername;
        Strokes = strokes;
        HoleNumber = holeNumber;
    }
}