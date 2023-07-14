using Shared.Model;

namespace Shared.Dtos.ScoreDto;

/// Data Transfer Object used when updating Score by a Member
public class ScoreBasicDto
{
    public string PlayerUsername { get; set; }   // Basically a user Id
    public List<int> Strokes { get; set; }
    public int GameId { get; set; }

    public ScoreBasicDto(string playerUsername, List<int> strokes, int gameId)
    {
        PlayerUsername = playerUsername;
        Strokes = strokes;
        GameId = gameId;
    }
}