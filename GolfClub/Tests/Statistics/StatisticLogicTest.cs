using Application.DaoInterfaces;
using Application.Logic;
using Microsoft.VisualBasic;
using Moq;
using NUnit.Framework;
using Shared.Dtos.TournamentDto;

namespace Tests.Statistics;

[TestFixture]
public class StatisticLogicTest
{
    private Mock<IStatisticDao> statisticDaoMock;

    private StatisticLogic statisticLogic;

    [SetUp]
    public void Setup()
    {
        statisticDaoMock = new Mock<IStatisticDao>();
        statisticLogic = new StatisticLogic(statisticDaoMock.Object);
    }
    
    [Test]
    public void GetAllScoresByPlayerTest_Z()
    {
        // Arrange
        string name = "Cedric";
        ICollection<Shared.Model.Score> scores = new List<Shared.Model.Score>();

        // Act
        statisticDaoMock.Setup(s => s.GetAllScoresByPlayerAsync(name)).Returns(Task.FromResult<IEnumerable<Shared.Model.Score>>(scores));
        var response = statisticLogic.GetAllScoresByPlayerAsync(name);

        // Assert
        Assert.That(response.Result, Is.EqualTo(scores));
        Assert.DoesNotThrowAsync(() => statisticLogic.GetAllScoresByPlayerAsync(name));
    }
    
    [Test]
    public void GetAllScoresByPlayerTest_O()
    {
        // Arrange
        string name = "Cedric";
        ICollection<Shared.Model.Score> scores = new List<Shared.Model.Score>();
        for (int i = 1; i <= 18; i++)
        {
            scores.Add(new Shared.Model.Score(name,i,2));
        }

        // Act
        statisticDaoMock.Setup(s => s.GetAllScoresByPlayerAsync(name)).Returns(Task.FromResult<IEnumerable<Shared.Model.Score>>(scores));
        var response = statisticLogic.GetAllScoresByPlayerAsync(name);

        // Assert
        Assert.That(response.Result, Is.EqualTo(scores));
        Assert.DoesNotThrowAsync(() => statisticLogic.GetAllScoresByPlayerAsync(name));
    }
    
    [Test]
    public void GetAllScoresByPlayerTest_M()
    {
        // Arrange
        string name = "Cedric";
        ICollection<Shared.Model.Score> scores = new List<Shared.Model.Score>();
        for (int i = 1; i <= 18; i++)
        {
            scores.Add(new Shared.Model.Score(name,i,2));
        }

        for (int i = 1; i <= 18; i++)
        {
            scores.Add(new Shared.Model.Score(name,i,4));
        }

        // Act
        statisticDaoMock.Setup(s => s.GetAllScoresByPlayerAsync(name)).Returns(Task.FromResult<IEnumerable<Shared.Model.Score>>(scores));
        var response = statisticLogic.GetAllScoresByPlayerAsync(name);

        // Assert
        Assert.That(response.Result, Is.EqualTo(scores));
        Assert.DoesNotThrowAsync(() => statisticLogic.GetAllScoresByPlayerAsync(name));
    }
    
    [Test]
    public void GetAllScoresByTournamentTest_Z()
    {
        // Arrange
        string name = "Cedric";
        string tournamentName = "Steve Grand Prix";
        ICollection<Shared.Model.Score> scores = new List<Shared.Model.Score>();

        // Act
        statisticDaoMock.Setup(s => s.GetAllScoresByTournamentAsync(tournamentName)).Returns(Task.FromResult<IEnumerable<Shared.Model.Score>>(scores));
        var response = statisticLogic.GetAllScoresByTournamentAsync(tournamentName);

        // Assert
        Assert.That(response.Result, Is.EqualTo(scores));
        Assert.DoesNotThrowAsync(() => statisticLogic.GetAllScoresByPlayerAsync(tournamentName));
    }
    
    [Test]
    public void GetAllScoresByTournamentTest_O()
    {
        // Arrange
        string name = "Cedric";
        string tournamentName = "Steve Grand Prix";
        ICollection<Shared.Model.Score> scores = new List<Shared.Model.Score>();
        for (int i = 1; i <= 18; i++)
        {
            scores.Add(new Shared.Model.Score(name,i,2));
        }

        // Act
        statisticDaoMock.Setup(s => s.GetAllScoresByTournamentAsync(tournamentName)).Returns(Task.FromResult<IEnumerable<Shared.Model.Score>>(scores));
        var response = statisticLogic.GetAllScoresByTournamentAsync(tournamentName);

        // Assert
        Assert.That(response.Result, Is.EqualTo(scores));
        Assert.DoesNotThrowAsync(() => statisticLogic.GetAllScoresByPlayerAsync(tournamentName));
    }
    
    [Test]
    public void GetAllScoresByTournamentTest_M()
    {
        // Arrange
        string name = "Cedric";
        string tournamentName = "Steve Grand Prix";
        ICollection<Shared.Model.Score> scores = new List<Shared.Model.Score>();
        for (int i = 1; i <= 18; i++)
        {
            scores.Add(new Shared.Model.Score(name,i,2));
        }
        for (int i = 1; i <= 18; i++)
        {
            scores.Add(new Shared.Model.Score(name,i,2));
        }

        // Act
        statisticDaoMock.Setup(s => s.GetAllScoresByTournamentAsync(tournamentName)).Returns(Task.FromResult<IEnumerable<Shared.Model.Score>>(scores));
        var response = statisticLogic.GetAllScoresByTournamentAsync(tournamentName);

        // Assert
        Assert.That(response.Result, Is.EqualTo(scores));
        Assert.DoesNotThrowAsync(() => statisticLogic.GetAllScoresByPlayerAsync(tournamentName));
    }
}