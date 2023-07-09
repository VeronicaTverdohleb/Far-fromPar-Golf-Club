namespace Shared.Dtos.TournamentDto;

public class RegisterPlayerDto
{

    public string PlayerName { get; set; }
    public string TournamentName { get; set; }
    
    public RegisterPlayerDto(string playerName, string tournamentName)
    {
        PlayerName = playerName;
        TournamentName = tournamentName;
    }
}