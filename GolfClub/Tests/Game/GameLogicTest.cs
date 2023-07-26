using Application.DaoInterfaces;
using Application.Logic;
using Moq;
using NUnit.Framework;
using Shared.Dtos.GameDto;
using Shared.Model;

namespace Tests.Game;

[TestFixture]
public class GameLogicTest
{
    private Mock<IGameDao> gameDaoMock;
    private Mock<IUserDao> userDaoMock;

    private GameLogic gameLogic;

    [SetUp]
    public void Setup()
    {
        gameDaoMock = new Mock<IGameDao>();
        userDaoMock = new Mock<IUserDao>();
        gameLogic = new GameLogic(gameDaoMock.Object, userDaoMock.Object);
    }
    
    // Tests for CreateAsync in GameLogic
    [Test]
    public void CreateAsyncTest_O()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        string[] usernames = { user.UserName };
        ICollection<string> playerUsernames = usernames.ToList();
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);

        Shared.Model.Score score1 = new Shared.Model.Score("Petra123", 2, 5);
        ICollection<Shared.Model.Score> scores = new List<Shared.Model.Score>();
        scores.Add(score1);
        Shared.Model.Game game = new Shared.Model.Game(scores, null, users);
        ICollection<Shared.Model.Game> games = new List<Shared.Model.Game>();
        games.Add(game);
        GameBasicDto gameDto = new GameBasicDto(null, null, playerUsernames, "Sunday Tournament");
        
        // Act 
        gameDaoMock.Setup(g => g.CreateAsync(gameDto)).Returns(Task.FromResult(game));
        gameDaoMock.Setup(ga => ga.GetGamesByUsername("Petra123"))
            .Returns(Task.FromResult<IEnumerable<Shared.Model.Game>>(games));

        // Assert
        var response = gameLogic.CreateAsync(gameDto);
        Assert.That(response.Result!, Is.EqualTo(game));
        Assert.DoesNotThrowAsync(() => gameLogic.CreateAsync(gameDto));
    }

    [Test]
    public void CreateAsyncTest_M()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        string[] usernames = { user.UserName };
        ICollection<string> playerUsernames = usernames.ToList();
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);

        Shared.Model.Score score1 = new Shared.Model.Score("Petra123", 2, 5);
        ICollection<Shared.Model.Score> scores = new List<Shared.Model.Score>();
        scores.Add(score1);
        Shared.Model.Game game = new Shared.Model.Game(scores, null, users);
        ICollection<Shared.Model.Game> games = new List<Shared.Model.Game>();
        games.Add(game);
        GameBasicDto gameDto = new GameBasicDto(null, null, playerUsernames, "Sunday Tournament");
        
        // Act 
        gameDaoMock.Setup(g => g.CreateAsync(gameDto)).Returns(Task.FromResult(game));
        gameDaoMock.Setup(ga => ga.GetGamesByUsername("Petra123"))
            .Returns(Task.FromResult<IEnumerable<Shared.Model.Game>>(games));

        // Assert
        var response1 = gameLogic.CreateAsync(gameDto);
        Assert.That(response1.Result!, Is.EqualTo(game));
        Assert.DoesNotThrowAsync(() => gameLogic.CreateAsync(gameDto));
        
        var response2 = gameLogic.CreateAsync(gameDto);
        Assert.That(response2.Result!, Is.EqualTo(game));
        Assert.DoesNotThrowAsync(() => gameLogic.CreateAsync(gameDto));
    }
    
    [Test]
    public void CreateAsyncTest_E()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        string[] usernames = { user.UserName };
        ICollection<string> playerUsernames = usernames.ToList();
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);

        Shared.Model.Score score1 = new Shared.Model.Score("Petra123", 2, 0);
        ICollection<Shared.Model.Score> scores = new List<Shared.Model.Score>();
        scores.Add(score1);
        Shared.Model.Game game = new Shared.Model.Game(scores, null, users);
        ICollection<Shared.Model.Game> games = new List<Shared.Model.Game>();
        games.Add(game);
        GameBasicDto gameDto = new GameBasicDto(null, null, playerUsernames, "Sunday Tournament");
        
        // Act 
        gameDaoMock.Setup(g => g.CreateAsync(gameDto)).Returns(Task.FromResult(game));
        gameDaoMock.Setup(ga => ga.GetGamesByUsername("Petra123"))
            .Returns(Task.FromResult<IEnumerable<Shared.Model.Game>>(games));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => gameLogic.CreateAsync(gameDto));
        Assert.That(e.Message, Is.EqualTo($"User with username {user.UserName} has an unfinished game. Cannot create a new game with this user."));
    }
    
    
    // Tests for GetActiveGameByUsernameAsync in GameLogic
    [Test]
    public void GetActiveGameByUsernameAsync_Z()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);
        
        Shared.Model.Score score1 = new Shared.Model.Score("Petra123", 2, 6);
        ICollection<Shared.Model.Score> scores = new List<Shared.Model.Score>();
        scores.Add(score1);
        
        Shared.Model.Game game = new Shared.Model.Game(scores, null, users);
        ICollection<Shared.Model.Game> games = new List<Shared.Model.Game>();
        games.Add(game);

        // Act
        gameDaoMock.Setup(ga => ga.GetGamesByUsername(user.UserName))
            .Returns(Task.FromResult<IEnumerable<Shared.Model.Game>>(games));
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));

        // Assert
        var response = gameLogic.GetActiveGameByUsernameAsync(user.UserName);
        Assert.That(response.Result!, Is.EqualTo(null));
        Assert.DoesNotThrowAsync(() => gameLogic.GetActiveGameByUsernameAsync(user.UserName));
    }
    
    
    [Test]
    public void GetActiveGameByUsernameAsync_O()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);

        Shared.Model.Score score1 = new Shared.Model.Score("Petra123", 2, 0);
        ICollection<Shared.Model.Score> scores = new List<Shared.Model.Score>();
        scores.Add(score1);
        Shared.Model.Game game = new Shared.Model.Game(scores, null, users);
        ICollection<Shared.Model.Game> games = new List<Shared.Model.Game>();
        games.Add(game);

        // Act
        gameDaoMock.Setup(ga => ga.GetGamesByUsername(user.UserName))
            .Returns(Task.FromResult<IEnumerable<Shared.Model.Game>>(games));
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));

        // Assert
        var response = gameLogic.GetActiveGameByUsernameAsync(user.UserName);
        Assert.That(response.Result!, Is.EqualTo(game));
        Assert.DoesNotThrowAsync(() => gameLogic.GetActiveGameByUsernameAsync(user.UserName));
    }

    [Test]
    public void GetActiveGameByUsernameAsync_E()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");

        // Act
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(null));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => gameLogic.GetActiveGameByUsernameAsync(user.UserName));
        Assert.That(e.Message, Is.EqualTo($"No user found"));
    }

    [Test]
    public void GetActiveGameByUsernameAsync_NoScores_O()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);
        
        Shared.Model.Game game = new Shared.Model.Game(null, null, users);
        ICollection<Shared.Model.Game> games = new List<Shared.Model.Game>();
        games.Add(game);

        // Act
        gameDaoMock.Setup(ga => ga.GetGamesByUsername(user.UserName))
            .Returns(Task.FromResult<IEnumerable<Shared.Model.Game>>(games));
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));

        // Assert
        var response = gameLogic.GetActiveGameByUsernameAsync(user.UserName);
        Assert.That(response.Result!, Is.EqualTo(game));
        Assert.DoesNotThrowAsync(() => gameLogic.GetActiveGameByUsernameAsync(user.UserName));
    }

    
    // Tests for GetAllGamesByUsernameAsync in GameLogic
    [Test]
    public void GetAllGamesByUsernameAsync_Z()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        
        // Act
        gameDaoMock.Setup(ga => ga.GetGamesByUsername(user.UserName))
            .Returns(Task.FromResult<IEnumerable<Shared.Model.Game>>(null));
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));
        
        // Assert
        var response = gameLogic.GetAllGamesByUsernameAsync(user.UserName);
        Assert.That(response.Result!, Is.EqualTo(null));
    }
    
    [Test]
    public void GetAllGamesByUsernameAsync_O()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);

        Shared.Model.Game game = new Shared.Model.Game(null, null, users);
        ICollection<Shared.Model.Game> games = new List<Shared.Model.Game>();
        games.Add(game);
        
        // Act
        gameDaoMock.Setup(ga => ga.GetGamesByUsername(user.UserName))
            .Returns(Task.FromResult<IEnumerable<Shared.Model.Game>>(games));
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));
        
        // Assert
        var response = gameLogic.GetAllGamesByUsernameAsync(user.UserName);
        Assert.That(response.Result!, Is.EqualTo(games.AsEnumerable()));
        Assert.DoesNotThrowAsync(() => gameLogic.GetActiveGameByUsernameAsync(user.UserName));
    }
    
    
    [Test]
    public void GetAllGamesByUsernameAsync_M()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);

        Shared.Model.Game game1 = new Shared.Model.Game(null, null, users);
        Shared.Model.Game game2 = new Shared.Model.Game(null, null, users);
        ICollection<Shared.Model.Game> games = new List<Shared.Model.Game>();
        games.Add(game1);
        games.Add(game2);
        
        // Act
        gameDaoMock.Setup(ga => ga.GetGamesByUsername(user.UserName))
            .Returns(Task.FromResult<IEnumerable<Shared.Model.Game>>(games));
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));
        
        // Assert
        var response = gameLogic.GetAllGamesByUsernameAsync(user.UserName);
        Assert.That(response.Result!, Is.EqualTo(games.AsEnumerable()));
        Assert.DoesNotThrowAsync(() => gameLogic.GetActiveGameByUsernameAsync(user.UserName));
    }


    [Test]
    public void GetAllGamesByUsernameAsync_E()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");

        // Act
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(null));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => gameLogic.GetAllGamesByUsernameAsync(user.UserName));
        Assert.That(e.Message, Is.EqualTo($"No user found"));
    }
    
    
    // Tests for GetGameByIdAsync in GameLogic
    [Test]
    public void GetGameByIdAsync_O()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);

        Shared.Model.Game game = new Shared.Model.Game(null, null, users);
        
        // Act
        gameDaoMock.Setup(g => g.GetGameByIdAsync(1))
            .Returns(Task.FromResult<Shared.Model.Game?>(game));
        
        // Assert
        var response = gameLogic.GetGameByIdAsync(1);
        Assert.That(response.Result!, Is.EqualTo(game));
        Assert.DoesNotThrowAsync(() => gameLogic.GetGameByIdAsync(1));
    }

    [Test]  // This is both Zero and Exception scenario
    public void GetGameByIdAsync_E()
    {
        // Arrange
        // Act
        gameDaoMock.Setup(g => g.GetGameByIdAsync(1))
            .Returns(Task.FromResult<Shared.Model.Game?>(null));
        
        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => gameLogic.GetGameByIdAsync(1));
        Assert.That(e.Message, Is.EqualTo($"Game with id 1 not found"));
    }

    
    
    
    
    
    

    
}