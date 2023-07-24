using System.Collections;
using Application.DaoInterfaces;
using Application.Logic;
using Moq;
using NUnit.Framework;
using Shared.Dtos;

namespace Tests.User;

[TestFixture]
public class UserLogicTest
{
    private Mock<IUserDao> userDaoMock;
    
    private UserLogic userLogic;

    [SetUp]
    public void Setup()
    {
        userDaoMock = new Mock<IUserDao>();
        userLogic = new UserLogic(userDaoMock.Object);
    }

    
    [Test]
    public async Task CreateAsyncTest_O()
    {
        //Arrange
        Shared.Model.User user = new Shared.Model.User("Devlin","Cudevlin", "1234", "Member");
        UserCreationDto dto = new UserCreationDto("Devlin", "Cudevlin", "1234");

        //Act
        userDaoMock.Setup(m => m.CreateAsync(dto)).Returns(Task.FromResult(user));
        Shared.Model.User response = await userLogic.CreateAsync(dto);
        
        //Assert
        Assert.That(response, Is.EqualTo(user));
        Assert.DoesNotThrowAsync(() => userLogic.CreateAsync(dto));

    }

    [Test] public async Task CreateAsyncTest_EmptyName()
    {
        //Arrange
        Shared.Model.User user = new Shared.Model.User("","Cudevlin", "1234", "Member");
        UserCreationDto dto = new UserCreationDto("", "Cudevlin", "1234");

        //Act
        userDaoMock.Setup(m => m.CreateAsync(dto)).Returns(Task.FromResult<Shared.Model.User>(null));

        //Assert
        var e = Assert.ThrowsAsync<Exception>(() => userLogic.CreateAsync(dto));
        Assert.That(e.Message, Is.EqualTo($"Enter a name for the user!"));

    }
    
    [Test] public async Task CreateAsyncTest_EmptyUsername()
    {
        //Arrange
        Shared.Model.User user = new Shared.Model.User("Devlin","", "1234", "Member");
        UserCreationDto dto = new UserCreationDto("Devlin", "", "1234");

        //Act
        userDaoMock.Setup(m => m.CreateAsync(dto)).Returns(Task.FromResult<Shared.Model.User>(null));

        //Assert
        var e = Assert.ThrowsAsync<Exception>(() => userLogic.CreateAsync(dto));
        Assert.That(e.Message, Is.EqualTo($"Enter a username for the user!"));

    }
    
    [Test] public async Task CreateAsyncTest_EmptyPassword()
    {
        //Arrange
        Shared.Model.User user = new Shared.Model.User("Devlin","Cudevlin", "", "Member");
        UserCreationDto dto = new UserCreationDto("Devlin", "Cudevlin", "");

        //Act
        userDaoMock.Setup(m => m.CreateAsync(dto)).Returns(Task.FromResult<Shared.Model.User>(null));

        //Assert
        var e = Assert.ThrowsAsync<Exception>(() => userLogic.CreateAsync(dto));
        Assert.That(e.Message, Is.EqualTo($"Enter a password for the user!"));

    }
    /*
    [Test] public async Task CreateAsyncTest_UserAlreadyExists()
    {
        //Arrange
        Shared.Model.User user = new Shared.Model.User("Devlin","Cudevlin", "1234", "Member");
        UserCreationDto dto = new UserCreationDto("Devlin", "Cudevlin", "1234");
        UserCreationDto dto1 = new UserCreationDto("Steve", "Cudevlin", "1234");

        //Act
        userDaoMock.Setup(m => m.CreateAsync(dto)).Returns(Task.FromResult(user));
        userDaoMock.Setup(m => m.CreateAsync(dto1)).Returns(Task.FromResult<Shared.Model.User>(null));

        //Assert
        var e = Assert.ThrowsAsync<Exception>(() => userLogic.CreateAsync(dto1));
        Console.WriteLine(e);
        Assert.That(e.Message, Is.EqualTo("User already exists!"));

    }
    */
    
    [Test]
    public async Task GetByUsernameAsync_Z()
    {
        //Arrange

        //Act
        userDaoMock.Setup(u => u.GetByUsernameAsync("userName")).Returns(Task.FromResult<Shared.Model.User?>(null));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => userLogic.GetByUsernameAsync("userName"));
        Assert.That(e.Message, Is.EqualTo("User with name userName not found"));

    }
    
    [Test]
    public async Task GetByUsernameAsync_O()
    {
        //Arrange
        string username = "Bob";
        Shared.Model.User user = new Shared.Model.User("Bobby", "Bob", "1234", "Member");
        //Act
        userDaoMock.Setup(u => u.GetByUsernameAsync(username)).Returns(Task.FromResult(user));
        var response = userLogic.GetByUsernameAsync(username);
        //Arrange
        Assert.That(response.Result, Is.EqualTo(user));
        Assert.DoesNotThrowAsync(() => userLogic.GetByUsernameAsync(username));

    }

    [Test]
    public async Task GetAllUsersAsync_Z()
    {
        //Arrange
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        //Act
        userDaoMock.Setup(t => t.GetAllUsersAsync()).Returns(Task.FromResult<IEnumerable<Shared.Model.User>>(users));
        var response = userLogic.GetAllUsersAsync();
        //Arrange
        Assert.That(response.Result, Is.EqualTo(users));
        Assert.DoesNotThrowAsync(() => userLogic.GetAllUsersAsync());
    }
    
    [Test]
    public async Task GetAllUsersAsync_O()
    {
        //Arrange
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        string username = "Bob";
        Shared.Model.User user = new Shared.Model.User("BobbyL", "Lee", "1234", "Member");
        users.Add(user);
        //Act
        userDaoMock.Setup(t => t.GetAllUsersAsync()).Returns(Task.FromResult<IEnumerable<Shared.Model.User>>(users));
        var response = userLogic.GetAllUsersAsync();
        //Arrange
        Assert.That(response.Result, Is.EqualTo(users));
        Assert.DoesNotThrowAsync(() => userLogic.GetAllUsersAsync());
    }
    
    [Test]
    public async Task GetAllUsersAsync_M()
    {
        //Arrange
        ICollection<Shared.Model.User> users = new List<Shared.Model.User>();
        string username = "Bob";
        Shared.Model.User user = new Shared.Model.User("Bob", "L", "1234", "Member");
        string username1 = "Bob";
        Shared.Model.User user1 = new Shared.Model.User("Bobby", "Le", "1234", "Member");
        string username2 = "Bob";
        Shared.Model.User user2 = new Shared.Model.User("BobbyL", "Lee", "1234", "Member");
        users.Add(user);
        users.Add(user1);
        users.Add(user2);
        //Act
        userDaoMock.Setup(t => t.GetAllUsersAsync()).Returns(Task.FromResult<IEnumerable<Shared.Model.User>>(users));
        var response = userLogic.GetAllUsersAsync();
        //Arrange
        Assert.That(response.Result, Is.EqualTo(users));
        Assert.DoesNotThrowAsync(() => userLogic.GetAllUsersAsync());
    }
}