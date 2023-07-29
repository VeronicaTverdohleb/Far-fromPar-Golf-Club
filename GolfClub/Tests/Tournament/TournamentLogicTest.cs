using Application.DaoInterfaces;
using Application.Logic;
using Moq;
using NUnit.Framework;
using Shared.Dtos.GameDto;
using Shared.Dtos.TournamentDto;
using Shared.Model;

namespace Tests.Tournament;

[TestFixture]
public class TournamentLogicTest
{
    private Mock<ITournamentDao> tournamentDaoMock;

    private TournamentLogic tournamentLogic;

    [SetUp]
    public void Setup()
    {
        tournamentDaoMock = new Mock<ITournamentDao>();
        tournamentLogic = new TournamentLogic(tournamentDaoMock.Object);
    }
    
    //Test to create a new tournament
    [Test]
    public async Task CreateTournamentAsyncTest_O()
    {
        // Arrange
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = DateOnly.FromDateTime(DateTime.Today);
        end = end.AddDays(5);
        Shared.Model.Tournament tournament = new Shared.Model.Tournament("Steve Grand Prix", start, end);
        CreateTournamentDto dto = new CreateTournamentDto(tournament.Name, tournament.StartDate, tournament.EndDate);

        // Act
        tournamentDaoMock.Setup(t => t.CreateTournamentAsync(dto)).Returns(Task.FromResult(tournament));
        tournamentDaoMock.Setup(t => t.GetTournamentByNameAsync("Steve Grand Prix"))
            .Returns(Task.FromResult(tournament)!);
        var response = await tournamentLogic.CreateTournamentAsync(dto);
        Console.WriteLine(response);

        // Assert
        Assert.That(response, Is.EqualTo(tournament));
        Assert.DoesNotThrowAsync(() => tournamentLogic.CreateTournamentAsync(dto));
    }
    
    //Tests creating a tournament with no name
    [Test]
    public void CreateTournamentEmptyNameAsyncTest_O()
    {
        // Arrange
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = DateOnly.FromDateTime(DateTime.Today);
        end = end.AddDays(5);
        Shared.Model.Tournament tournament = new Shared.Model.Tournament("", start, end);
        CreateTournamentDto dto = new CreateTournamentDto(tournament.Name, tournament.StartDate, tournament.EndDate);

        // Act
        tournamentDaoMock.Setup(t => t.CreateTournamentAsync(dto))
            .Returns(Task.FromResult<Shared.Model.Tournament?>(null));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => tournamentLogic.CreateTournamentAsync(dto));
        Assert.That(e.Message, Is.EqualTo($"Enter a name for the tournament!"));
    }
    
    //Tests creating a tournament taking place in the past
    [Test]
    public void CreateTournamentInPastAsyncTest_O()
    {
        // Arrange
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = DateOnly.FromDateTime(DateTime.Today);
        start = start.AddDays(-10);
        end = end.AddDays(-5);
        Shared.Model.Tournament tournament = new Shared.Model.Tournament("Steve Grand Prix", start, end);
        CreateTournamentDto dto = new CreateTournamentDto(tournament.Name, tournament.StartDate, tournament.EndDate);

        // Act
        tournamentDaoMock.Setup(t => t.CreateTournamentAsync(dto))
            .Returns(Task.FromResult<Shared.Model.Tournament?>(null));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => tournamentLogic.CreateTournamentAsync(dto));
        Assert.That(e.Message, Is.EqualTo($"The tournament would already be over!"));
    }
    
    //Tests creating a tournament where the start date is after the end date
    [Test]
    public void CreateTournamentReversedDatesTest_O()
    {
        // Arrange
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = DateOnly.FromDateTime(DateTime.Today);
        start = start.AddDays(10);
        end = end.AddDays(5);
        Shared.Model.Tournament tournament = new Shared.Model.Tournament("Steve Grand Prix", start, end);
        CreateTournamentDto dto = new CreateTournamentDto(tournament.Name, tournament.StartDate, tournament.EndDate);

        // Act
        tournamentDaoMock.Setup(t => t.CreateTournamentAsync(dto))
            .Returns(Task.FromResult<Shared.Model.Tournament?>(null));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => tournamentLogic.CreateTournamentAsync(dto));
        Assert.That(e.Message, Is.EqualTo($"Incorrect start and end dates!"));
    }
    
    //Tests getting a tournament by its name
    [Test]
    public void GetTournamentByNameTest_O()
    {
        // Arrange
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = DateOnly.FromDateTime(DateTime.Today);
        string name = "Steve Grand Prix";
        Shared.Model.Tournament tournament = new Shared.Model.Tournament(name, start, end);
        ICollection<Shared.Model.Tournament> tournaments = new List<Shared.Model.Tournament>();
        tournaments.Add(tournament);
        CreateTournamentDto dto = new CreateTournamentDto(tournament.Name, tournament.StartDate, tournament.EndDate);

        // Act
        tournamentDaoMock.Setup(t => t.GetTournamentByNameAsync(name)).Returns(Task.FromResult(tournament));
        var response = tournamentLogic.GetTournamentByNameAsync(name);

        // Assert
        Assert.That(response.Result, Is.EqualTo(tournament));
        Assert.DoesNotThrowAsync(() => tournamentLogic.CreateTournamentAsync(dto));
    }
    
    //Tests getting a tournament by its name when it doesn't exist
    [Test]
    public void GetTournamentByNameTest_Z()
    {
        // Arrange

        // Act
        tournamentDaoMock.Setup(t => t.GetTournamentByNameAsync("testName")).Returns(Task.FromResult<Shared.Model.Tournament?>(null));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => tournamentLogic.GetTournamentByNameAsync("testName"));
        Assert.That(e.Message, Is.EqualTo($"Tournament with name testName not found"));
    }
    
    //Tests deleting an existing tournament
    [Test]
    public void DeleteTournament_O()
    {
        //Arrange
        
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = DateOnly.FromDateTime(DateTime.Today);
        string name = "Steve Grand Prix";
        Shared.Model.Tournament tournament = new Shared.Model.Tournament(name, start, end);
        ICollection<Shared.Model.Tournament> tournaments = new List<Shared.Model.Tournament>();
        tournaments.Add(tournament);
        CreateTournamentDto dto = new CreateTournamentDto(tournament.Name, tournament.StartDate, tournament.EndDate);
        
        //Act
        tournamentDaoMock.Setup(t => t.GetTournamentByNameAsync(name)).Returns(Task.FromResult<Shared.Model.Tournament?>(tournament));
        
        //Assert
        Assert.DoesNotThrowAsync(()=>tournamentLogic.DeleteTournamentAsync(name));
    }
    
    //Tests deleting a tournament that doesn't exist
    [Test]
    public void DeleteTournament_Z()
    {
        //Arrange

        //Act
        tournamentDaoMock.Setup(t => t.DeleteTournamentAsync("testName"));
        
        //Assert
        var e = Assert.ThrowsAsync<Exception>(() => tournamentLogic.DeleteTournamentAsync("testName"));
        Assert.That(e.Message, Is.EqualTo("Tournament with name testName doesn't exist"));
    }
    
    //Tests getting all tournaments when there are many
    [Test]
    public void GetAllTournamentsTest_M()
    {
        // Arrange
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = DateOnly.FromDateTime(DateTime.Today);
        string name = "Steve Grand Prix";
        string name2 = "Stevendon";
        string name3 = "Tour De Steve";
        Shared.Model.Tournament tournament = new Shared.Model.Tournament(name, start, end);
        Shared.Model.Tournament tournament2 = new Shared.Model.Tournament(name2, start, end);
        Shared.Model.Tournament tournament3 = new Shared.Model.Tournament(name3, start, end);
        ICollection<Shared.Model.Tournament> tournaments = new List<Shared.Model.Tournament>();
        tournaments.Add(tournament);
        tournaments.Add(tournament2);
        tournaments.Add(tournament3);
        CreateTournamentDto dto = new CreateTournamentDto(tournament.Name, tournament.StartDate, tournament.EndDate);
        CreateTournamentDto dto2 = new CreateTournamentDto(tournament2.Name, tournament2.StartDate, tournament2.EndDate);
        CreateTournamentDto dto3 = new CreateTournamentDto(tournament3.Name, tournament3.StartDate, tournament3.EndDate);

        // Act
        tournamentDaoMock.Setup(t => t.GetAllTournamentsAsync()).Returns(Task.FromResult<IEnumerable<Shared.Model.Tournament>>(tournaments));
        var response = tournamentLogic.GetAllTournamentsAsync();

        // Assert
        Assert.That(response.Result, Is.EqualTo(tournaments));
        Assert.DoesNotThrowAsync(() => tournamentLogic.GetAllTournamentsAsync());
    }
    
    //Tests getting all registered players to a tournament when there is one
    [Test]
    public void RegisterTournamentPlayerTest_O()
    {
        // Arrange
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = DateOnly.FromDateTime(DateTime.Today);
        string name = "Steve Grand Prix";
        Shared.Model.User player = new Shared.Model.User("Cedric", "Cedric", "test", "test");
        Shared.Model.Tournament tournament = new Shared.Model.Tournament(name, start, end);
        tournament.Players = new List<Shared.Model.User>();
        ICollection<Shared.Model.Tournament> tournaments = new List<Shared.Model.Tournament>();
        ICollection<Shared.Model.User> players = new List<Shared.Model.User>();
        tournaments.Add(tournament);
        players.Add(player);
        RegisterPlayerDto dto = new RegisterPlayerDto(player.UserName, tournament.Name);

        // Act
        tournamentDaoMock.Setup(t => t.GetTournamentByNameAsync(dto.TournamentName))
            .Returns(Task.FromResult<Shared.Model.Tournament?>(tournament));

        // Assert
        Assert.DoesNotThrowAsync(() => tournamentLogic.RegisterPlayerAsync(dto));
    }
    
    //Tests registering a player to a tournament that it is already registered to
    [Test]
    public void RegisterTournamentPlayerAlreadyRegisteredTest_O()
    {
        // Arrange
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = DateOnly.FromDateTime(DateTime.Today);
        string name = "Steve Grand Prix";
        Shared.Model.User player = new Shared.Model.User("Cedric", "Cedric", "test", "test");
        Shared.Model.Tournament tournament = new Shared.Model.Tournament(name, start, end);
        tournament.Players = new List<Shared.Model.User>();
        tournament.Players.Add(player);
        ICollection<Shared.Model.Tournament> tournaments = new List<Shared.Model.Tournament>();
        ICollection<Shared.Model.User> players = new List<Shared.Model.User>();
        tournaments.Add(tournament);
        players.Add(player);
        RegisterPlayerDto dto = new RegisterPlayerDto(player.UserName, tournament.Name);

        // Act
        tournamentDaoMock.Setup(t => t.GetTournamentByNameAsync(dto.TournamentName))
            .Returns(Task.FromResult<Shared.Model.Tournament?>(tournament));
        tournamentDaoMock.Setup(t => t.GetAllTournamentPlayersAsync(dto.TournamentName))
            .Returns(Task.FromResult<IEnumerable<Shared.Model.User>>(players));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => tournamentLogic.RegisterPlayerAsync(dto));
        Assert.That(e.Message, Is.EqualTo("This player is already registered!"));
    }
    
    //Tests getting the registered players in a tournament if there is one
    [Test]
    public void GetTournamentPlayersTest_O()
    {
        // Arrange
        DateOnly start = DateOnly.FromDateTime(DateTime.Today);
        DateOnly end = DateOnly.FromDateTime(DateTime.Today);
        string name = "Steve Grand Prix";
        Shared.Model.User player = new Shared.Model.User("Cedric", "Cedric", "test", "test");
        Shared.Model.Tournament tournament = new Shared.Model.Tournament(name, start, end);
        tournament.Players = new List<Shared.Model.User>();
        tournament.Players.Add(player);
        ICollection<Shared.Model.Tournament> tournaments = new List<Shared.Model.Tournament>();
        ICollection<Shared.Model.User> players = new List<Shared.Model.User>();
        tournaments.Add(tournament);
        players.Add(player);

        // Act
        tournamentDaoMock.Setup(t => t.GetAllTournamentPlayersAsync(tournament.Name))
            .Returns(Task.FromResult<IEnumerable<Shared.Model.User>>(players));
        var response = tournamentLogic.GetAllTournamentPlayersAsync(tournament.Name);

        // Assert
        Assert.That(response.Result, Is.EqualTo(players));
        Assert.DoesNotThrowAsync(() => tournamentLogic.GetAllTournamentPlayersAsync(tournament.Name));
    }
}