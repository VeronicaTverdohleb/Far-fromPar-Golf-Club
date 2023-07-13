namespace Shared.Dtos.ScoreDto;

public class ScoreUpdateDto
{
    public string PlayerUsername { get; set; }   // Basically a user Id
    public Dictionary<int, int> HolesAndStrokes { get; set; }
    public int GameId { get; set; }

    public ScoreUpdateDto(string playerUsername, Dictionary<int, int> holesAndStrokes, int gameId)
    {
        PlayerUsername = playerUsername;
        HolesAndStrokes = holesAndStrokes;
        GameId = gameId;
    }
}