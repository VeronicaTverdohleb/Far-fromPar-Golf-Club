namespace Shared.Model;

public class Tournament
{
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public ICollection<Game> Games { get; set; }
    
    private Tournament() {}
}