﻿using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface ITournamentService
{
    public Task CreateTournamentAsync(CreateTournamentDto dto);
    public Task<Tournament> GetTournamentByNameAsync(string name);
}