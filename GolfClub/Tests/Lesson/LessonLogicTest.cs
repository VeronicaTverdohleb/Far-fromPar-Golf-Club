using Application.DaoInterfaces;
using Application.Logic;
using Moq;
using NUnit.Framework;
using Shared.Dtos.LessonDto;
using Shared.Model;


[TestFixture]
public class LessonLogicTest
{
    private Mock<ILessonDao> lessonDaoMock;
    private Mock<IUserDao> userDaoMock;
    
    private LessonLogic lessonLogic;

    [SetUp]
    public void Setup()
    {
        lessonDaoMock = new Mock<ILessonDao>();
        userDaoMock = new Mock<IUserDao>();
        lessonLogic = new LessonLogic(lessonDaoMock.Object, userDaoMock.Object);
    }
    
    [Test]
    public async Task CreateAsyncTest_O()
    {
        //Arrange
        User user = new User("Devlin", "CuDevlin", "1234", "Member");
        LessonCreationDto dto = new LessonCreationDto("2023-07-21", "10.00", "CuDevlin", "Petra");
        DateOnly dateOnly = new DateOnly(2023, 07, 21);
        Lesson lesson = new Lesson(dateOnly, "10.00", user, "Petra");
        
        //Act
        lessonDaoMock.Setup(l => l.CreateAsync(dto, user)).Returns(Task.FromResult(lesson));
        userDaoMock.Setup(lu => lu.GetByUsernameAsync("CuDevlin")).Returns(Task.FromResult<User?>(user));
        Lesson response = await lessonLogic.CreateAsync(dto);

        //Assert
        Assert.That(response, Is.EqualTo(lesson));
        Assert.DoesNotThrowAsync(() => lessonLogic.CreateAsync(dto));

    }
    
    [Test]
    public async Task CreateAsyncTest_M()
    {
        //Arrange
        User user = new User("Devlin", "CuDevlin", "1234", "Member");
        User user2 = new User("Cedric", "BigC", "1234", "Member");
        
        LessonCreationDto dto = new LessonCreationDto("2023-07-21", "10.00", "CuDevlin", "Petra");
        LessonCreationDto dto1 = new LessonCreationDto("2023-07-21", "12.00", "BigC", "Veronica");
        
        DateOnly dateOnly = new DateOnly(2023, 07, 21);
        Lesson lesson = new Lesson(dateOnly, "10.00", user, "Petra");
        Lesson lesson1 = new Lesson(dateOnly, "12.00", user, "Veronica");
        
        //Act
        lessonDaoMock.Setup(l => l.CreateAsync(dto, user)).Returns(Task.FromResult(lesson));
        lessonDaoMock.Setup(l1 => l1.CreateAsync(dto1, user2)).Returns(Task.FromResult(lesson1));
        
        userDaoMock.Setup(lu => lu.GetByUsernameAsync("CuDevlin")).Returns(Task.FromResult<User?>(user));
        userDaoMock.Setup(lu1 => lu1.GetByUsernameAsync("BigC")).Returns(Task.FromResult<User?>(user2));
        
        Lesson response = await lessonLogic.CreateAsync(dto);
        Lesson response1 = await lessonLogic.CreateAsync(dto1);

        //Assert
        Assert.That(response, Is.EqualTo(lesson));
        Assert.DoesNotThrowAsync(() => lessonLogic.CreateAsync(dto));
        Assert.That(response1, Is.EqualTo(lesson1));
        Assert.DoesNotThrowAsync(() => lessonLogic.CreateAsync(dto1));
    }

    [Test]
    public async Task CreateAsync_UserNotFound()
    {
        User user = new User("Devlin", "CuDevlin", "1234", "Member");
        LessonCreationDto dto = new LessonCreationDto("2023-07-21", "10.00", "CuDevlin", "Petra");
        DateOnly dateOnly = new DateOnly(2023, 07, 21);
        Lesson lesson = new Lesson(dateOnly, "10.00", user, "Petra");
        
        //Act
        lessonDaoMock.Setup(l => l.CreateAsync(dto, user)).Returns(Task.FromResult(lesson));
        userDaoMock.Setup(lu => lu.GetByUsernameAsync("CuDevlin")).Returns(Task.FromResult<User?>(null));


        //Assert
        var e = Assert.ThrowsAsync<Exception>(() => lessonLogic.CreateAsync(dto));
        Assert.That(e.Message, Is.EqualTo("User not found"));
    }
}