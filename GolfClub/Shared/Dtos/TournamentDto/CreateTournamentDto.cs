using Shared.Model;

namespace Shared.Dtos.TournamentDto;

/// <summary>
/// Data Transfer Object used when creating a new tournament
/// </summary>
public class CreateTournamentDto
{
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public CreateTournamentDto(string name, DateOnly startDate, DateOnly endDate)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
    }
}