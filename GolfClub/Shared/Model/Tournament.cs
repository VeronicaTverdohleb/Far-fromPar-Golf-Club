﻿namespace Shared.Model;

public class Tournament
{
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public ICollection<Game>? Games { get; set; }

    public Tournament(string name, DateOnly startDate, DateOnly endDate)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        Games = new List<Game>();
    }
    private Tournament() {}
}