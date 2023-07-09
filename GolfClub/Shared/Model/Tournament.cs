using System.Text.Json.Serialization;

namespace Shared.Model;

public class Tournament
{
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    
    [JsonIgnore]
    public ICollection<Game>? Games { get; set; }
    [JsonIgnore]
    public ICollection<User>? Players { get; set;}

    public Tournament(string name, DateOnly startDate, DateOnly endDate)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        Games = new List<Game>();
        Players = new List<User>();
    }
    private Tournament() {}
}