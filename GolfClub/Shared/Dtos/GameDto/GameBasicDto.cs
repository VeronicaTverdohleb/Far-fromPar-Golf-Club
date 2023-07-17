using Shared.Model;

namespace Shared.Dtos.GameDto;

/// <summary>
/// Data Transfer Object used in Game-related use cases
/// </summary>
public class GameBasicDto
{ 
    public ICollection<Score>? Scores { get; set; }
    public ICollection<Equipment>? Equipments { get; set; }
    public ICollection<string> PlayerUsernames { get; set; }
    
    public string? TournamentName { get; set; }
    
    public GameBasicDto(ICollection<Score>? scores, ICollection<Equipment>? equipments, ICollection<string> playerUsernames, string? tournamentName)
    {
        Scores = scores;
        Equipments = equipments;
        PlayerUsernames = playerUsernames;
        TournamentName = tournamentName;
    }
}