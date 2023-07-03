using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace Application.Logic;

public class TournamentLogic:ITournamentLogic
{
    private readonly ITournamentDao tournamentDao;

    public TournamentLogic(ITournamentDao tournamentDao)
    {
        this.tournamentDao = tournamentDao;
    }
    
    public async Task<Tournament> CreateTournamentAsync(CreateTournamentDto dto)
    {
        Tournament newTournament = new Tournament(dto.Name, dto.StartDate, dto.EndDate);
        Tournament created = await tournamentDao.CreateTournamentAsync(newTournament);
        return created;
    }

    public async Task<Tournament> GetTournamentByNameAsync(string name)
    {
        Tournament? tournament = await tournamentDao.GetTournamentByNameAsync(name);
        if (tournament == null)
        {
            throw new Exception($"Tournament with name {name} not found");
        }

        return tournament;
    }
}