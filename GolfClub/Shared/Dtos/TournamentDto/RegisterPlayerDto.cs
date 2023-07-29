namespace Shared.Dtos.TournamentDto;

/// <summary>
/// Data Transfer Object used when registering a player to a tournament
/// </summary>
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