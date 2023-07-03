namespace Shared.Model;

public class Game
{
    public int Id { get; set; }
    public int? TournamentId { get; set; }
    public ICollection<Score> Scores { get; set; }
    public ICollection<Equipment> Equipments { get; set; }
    public ICollection<User> Players { get; set; }
}