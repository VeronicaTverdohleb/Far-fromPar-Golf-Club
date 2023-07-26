using Application.DaoInterfaces;
using Application.Logic;
using Moq;
using NUnit.Framework;
using Shared.Dtos.ScoreDto;
using Shared.Model;

namespace Tests.Score;

[TestFixture]
public class ScoreLogicTest
{
    private Mock<IScoreDao> scoreDaoMock;
    private Mock<IGameDao> gameDaoMock;
    private Mock<IUserDao> userDaoMock;

    private ScoreLogic scoreLogic;

    [SetUp]
    public void Setup()
    {
        scoreDaoMock = new Mock<IScoreDao>();
        gameDaoMock = new Mock<IGameDao>();
        userDaoMock = new Mock<IUserDao>();
        scoreLogic = new ScoreLogic(scoreDaoMock.Object, userDaoMock.Object, gameDaoMock.Object);
    }
    
    
    // Tests for UpdateFromMemberAsync in GameLogic
    [Test]
    public void UpdateFromMemberAsync_O()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);
        Shared.Model.Game game = new Shared.Model.Game(null, null, users) {Id = 1};
        int[] strokes = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 
            11, 12, 13, 14, 15, 16, 17, 18 };  // Has to be 18 strokes because the Member can only update all 18 at a time
        ScoreBasicDto dto = new ScoreBasicDto(user.UserName, strokes.ToList(), 1);
        
        // Act
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));
        gameDaoMock.Setup(g => g.GetGameByIdAsync(game.Id))
            .Returns(Task.FromResult<Shared.Model.Game?>(game));
        
        // Assert - Update does not return anything so we only check that it does not throw Exception
        Assert.DoesNotThrowAsync(() => scoreLogic.UpdateFromMemberAsync(dto));
    }


    [Test]
    public void UpdateFromMember_M()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);
        Shared.Model.Game game1 = new Shared.Model.Game(null, null, users) {Id = 1};
        Shared.Model.Game game2 = new Shared.Model.Game(null, null, users) {Id = 2};
        int[] strokes = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 
            11, 12, 13, 14, 15, 16, 17, 18 };  // Has to be 18 strokes because the Member can only update all 18 at a time
        ScoreBasicDto dto1 = new ScoreBasicDto(user.UserName, strokes.ToList(), game1.Id);
        ScoreBasicDto dto2 = new ScoreBasicDto(user.UserName, strokes.ToList(), game2.Id);
        
        // Act
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));
        gameDaoMock.Setup(g => g.GetGameByIdAsync(game1.Id))
            .Returns(Task.FromResult<Shared.Model.Game?>(game1));
        gameDaoMock.Setup(g => g.GetGameByIdAsync(game2.Id))
            .Returns(Task.FromResult<Shared.Model.Game?>(game2));
        
        
        // Assert - Update does not return anything so we only check that it does not throw Exception
        Assert.DoesNotThrowAsync(() => scoreLogic.UpdateFromMemberAsync(dto1));
        Assert.DoesNotThrowAsync(() => scoreLogic.UpdateFromMemberAsync(dto2));

    }
    
    
    [Test]
    public void UpdateFromMemberAsync_B()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);
        int[] strokes = { 0, 0, 0, 25, 27, 28, 29, 30, 20, 20, 
            20, 12, 13, 14, 15, 16, 17, 18 };  // Some strokes are out of the range of allowed number of strokes
        
        Shared.Model.Game game = new Shared.Model.Game(null, null, users) {Id = 1};
        ScoreBasicDto dto = new ScoreBasicDto(user.UserName, strokes.ToList(), 1);
        
        // Act
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));
        gameDaoMock.Setup(g => g.GetGameByIdAsync(1))
            .Returns(Task.FromResult<Shared.Model.Game?>(game));

        // Assert - Update does not return anything so we only check that it does not throw Exception
        Assert.DoesNotThrowAsync(() => scoreLogic.UpdateFromMemberAsync(dto));
    }    
    
    
    [Test]
    public void UpdateFromMemberAsync_E_UserNotFound()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        int[] strokes = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 
            11, 12, 13, 14, 15, 16, 17, 18 };  // Has to be 18 strokes because the Member can only update all 18 at a time
        ScoreBasicDto dto = new ScoreBasicDto(user.UserName, strokes.ToList(), 1);
        
        // Act
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(null));

        // Assert 
        var e = Assert.ThrowsAsync<Exception>(() => scoreLogic.UpdateFromMemberAsync(dto));
        Assert.That(e.Message, Is.EqualTo($"User with username {user.UserName} does not exist"));
    }

    
    [Test]
    public void UpdateFromMemberAsync_E_GameNotFound()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        int[] strokes = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 
            11, 12, 13, 14, 15, 16, 17, 18 };  // Has to be 18 strokes because the Member can only update all 18 at a time
        ScoreBasicDto dto = new ScoreBasicDto(user.UserName, strokes.ToList(), 1);
        
        // Act
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));
        gameDaoMock.Setup(g => g.GetGameByIdAsync(1))
            .Returns(Task.FromResult<Shared.Model.Game?>(null));

        // Assert 
        var e = Assert.ThrowsAsync<Exception>(() => scoreLogic.UpdateFromMemberAsync(dto));
        Assert.That(e.Message, Is.EqualTo($"Game with id {dto.GameId} does not exist"));
    }

    
    // Tests for UpdateFromEmployeeAsync in GameLogic
    [Test]
    public void UpdateFromEmployeeAsync_O()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);
        Shared.Model.Game game = new Shared.Model.Game(null, null, users) {Id = 1};
        Dictionary<int, int> strokesAndHoles = new Dictionary<int, int>();
        strokesAndHoles.Add(1, 10); // One score only in this Test Case
        ScoreUpdateDto dto = new ScoreUpdateDto(user.UserName, strokesAndHoles, game.Id);
        
        // Act
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));
        gameDaoMock.Setup(g => g.GetGameByIdAsync(game.Id))
            .Returns(Task.FromResult<Shared.Model.Game?>(game));
        
        // Assert - Update does not return anything so we only check that it does not throw Exception
        Assert.DoesNotThrowAsync(() => scoreLogic.UpdateFromEmployeeAsync(dto));
    }

    [Test]
    public void UpdateFromEmployeeAsync_M()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);
        Shared.Model.Game game = new Shared.Model.Game(null, null, users) {Id = 1};
        Dictionary<int, int> strokesAndHoles = new Dictionary<int, int>();
        strokesAndHoles.Add(1, 10); // Many scores
        strokesAndHoles.Add(2, 11);
        strokesAndHoles.Add(3, 15);
        ScoreUpdateDto dto = new ScoreUpdateDto(user.UserName, strokesAndHoles, game.Id);
        
        // Act
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));
        gameDaoMock.Setup(g => g.GetGameByIdAsync(game.Id))
            .Returns(Task.FromResult<Shared.Model.Game?>(game));
        
        // Assert - Update does not return anything so we only check that it does not throw Exception
        Assert.DoesNotThrowAsync(() => scoreLogic.UpdateFromEmployeeAsync(dto));
    }


    [Test]
    public void UpdateFromEmployee_B_TooManyScores()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);
        Shared.Model.Game game = new Shared.Model.Game(null, null, users) {Id = 1};
        Dictionary<int, int> strokesAndHoles = new Dictionary<int, int>();
        strokesAndHoles.Add(1, 1);  // 19 scores which is more than the amount 
        strokesAndHoles.Add(2, 2);  // Of holes, so it should throw Exception
        strokesAndHoles.Add(3, 3);
        strokesAndHoles.Add(4, 4);
        strokesAndHoles.Add(5, 5);
        strokesAndHoles.Add(6, 6);
        strokesAndHoles.Add(7, 7);
        strokesAndHoles.Add(8, 8);
        strokesAndHoles.Add(9, 9);
        strokesAndHoles.Add(10, 10);
        strokesAndHoles.Add(11, 11);
        strokesAndHoles.Add(12, 12);
        strokesAndHoles.Add(13, 13);
        strokesAndHoles.Add(14, 14);
        strokesAndHoles.Add(15, 15);
        strokesAndHoles.Add(16, 16);
        strokesAndHoles.Add(17, 17);
        strokesAndHoles.Add(18, 18);
        strokesAndHoles.Add(19, 19);
        
        ScoreUpdateDto dto = new ScoreUpdateDto(user.UserName, strokesAndHoles, game.Id);
        
        // Act
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));
        gameDaoMock.Setup(g => g.GetGameByIdAsync(game.Id))
            .Returns(Task.FromResult<Shared.Model.Game?>(game));
        
        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => scoreLogic.UpdateFromEmployeeAsync(dto));
        Assert.That(e.Message, Is.EqualTo($"Too many scores! There are only 18 holes."));
    }


    [Test]
    public void UpdateFromEmployee_B_WrongHoleNumber()
    {
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        users.Add(user);
        Shared.Model.Game game = new Shared.Model.Game(null, null, users) {Id = 1};
        Dictionary<int, int> strokesAndHoles = new Dictionary<int, int>();
        strokesAndHoles.Add(0, 1);  // Wrong hole number

        ScoreUpdateDto dto = new ScoreUpdateDto(user.UserName, strokesAndHoles, game.Id);
        
        // Act
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));
        gameDaoMock.Setup(g => g.GetGameByIdAsync(game.Id))
            .Returns(Task.FromResult<Shared.Model.Game?>(game));
        
        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => scoreLogic.UpdateFromEmployeeAsync(dto));
        Assert.That(e.Message, Is.EqualTo($"The hole with Id 0 does not exist"));
    }
    

    [Test]
    public void UpdateFromEmployee_E_UserNotFound()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        Dictionary<int, int> strokesAndHoles = new Dictionary<int, int>();
        strokesAndHoles.Add(1, 10); // One score only in this Test Case
        ScoreUpdateDto dto = new ScoreUpdateDto(user.UserName, strokesAndHoles, 1);
        
        // Act
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(null));

        // Assert 
        var e = Assert.ThrowsAsync<Exception>(() => scoreLogic.UpdateFromEmployeeAsync(dto));
        Assert.That(e.Message, Is.EqualTo($"User with username {user.UserName} does not exist"));
    }

    [Test]
    public void UpdateFromEmployee_E_GameNotFound()
    {
        // Arrange
        Shared.Model.User user = new Shared.Model.User("Petra Hrabakova", "Petra123", "123", "Member");
        Dictionary<int, int> strokesAndHoles = new Dictionary<int, int>();
        strokesAndHoles.Add(1, 10); // One score only in this Test Case
        ScoreUpdateDto dto = new ScoreUpdateDto(user.UserName, strokesAndHoles, 1);
        
        // Act
        userDaoMock.Setup(u => u.GetByUsernameAsync(user.UserName))
            .Returns(Task.FromResult<Shared.Model.User?>(user));
        gameDaoMock.Setup(g => g.GetGameByIdAsync(1))
            .Returns(Task.FromResult<Shared.Model.Game?>(null));

        // Assert 
        var e = Assert.ThrowsAsync<Exception>(() => scoreLogic.UpdateFromEmployeeAsync(dto));
        Assert.That(e.Message, Is.EqualTo($"Game with id {dto.GameId} does not exist"));
    }
}