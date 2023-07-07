using Shared.Model;

namespace Shared.Dtos.GameDto;

public class GameBasicDto
{ 
    public ICollection<Score>? Scores { get; set; }
    public ICollection<Equipment>? Equipments { get; set; }
    public ICollection<User> Players { get; set; }
    
    public GameBasicDto(ICollection<Score>? scores, ICollection<Equipment>? equipments, ICollection<User> players)
    {
        Scores = scores;
        Equipments = equipments;
        Players = players;
    }
}