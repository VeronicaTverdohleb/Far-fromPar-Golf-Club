namespace Shared.Model;

public class Game
{
    public int Id { get; set; }
    public ICollection<Score>? Scores { get; set; }
    public ICollection<Equipment>? Equipments { get; set; }
    public ICollection<User> Players { get; set; }

    public Game(ICollection<Score>? scores, ICollection<Equipment>? equipments, ICollection<User> players)
    {
        Scores = scores;
        Equipments = equipments;
        Players = players;
    }

    private Game() {}
}