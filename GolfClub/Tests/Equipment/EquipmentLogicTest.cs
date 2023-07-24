using Application.DaoInterfaces;
using Application.Logic;
using Moq;
using NUnit.Framework;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace Tests.Equipment;

[TestFixture]
public class EquipmentLogicTest
{
    private Mock<IGameDao> gameDaoMock;
    private Mock<IEquipmentDao> equipmentDaoMock;

    private EquipmentLogic equipmentLogic;
    
    [SetUp]
    public void Setup()
    {
        gameDaoMock = new Mock<IGameDao>();
        equipmentDaoMock = new Mock<IEquipmentDao>();
        equipmentLogic = new EquipmentLogic(equipmentDaoMock.Object,gameDaoMock.Object);
    }
    
    //Tests for CreateEquipmentAsync in EquipmentLogic

    [Test]
    public void CreateEquipmentAsyncTest_O()
    {
        EquipmentBasicDto e1 = new EquipmentBasicDto("Golf ball");
        ICollection<EquipmentBasicDto> equipments = new List<EquipmentBasicDto>();
        equipments.Add(e1);
        int amount = 1;
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        eqToList.Add(toAdd);
        equipmentDaoMock.Setup(e => e.CreateEquipmentAsync(equipments, amount)).Returns(Task.FromResult(eq));
        var response = equipmentLogic.CreateEquipmentAsync(equipments, amount);
        Assert.That(response.Result!,Is.EqualTo(eq));
        Assert.DoesNotThrowAsync(()=>equipmentLogic.CreateEquipmentAsync(equipments, amount));


    }
    
    [Test]
    public void CreateEquipmentAsyncTest_M()
    {
        EquipmentBasicDto e1 = new EquipmentBasicDto("Golf ball");
        ICollection<EquipmentBasicDto> equipments = new List<EquipmentBasicDto>();
        equipments.Add(e1);
        int amount = 3;
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        eqToList.Add(toAdd);
        equipmentDaoMock.Setup(e => e.CreateEquipmentAsync(equipments, amount)).Returns(Task.FromResult(eq));
        
        var response1 = equipmentLogic.CreateEquipmentAsync(equipments, amount);
        Assert.That(response1.Result!,Is.EqualTo(eq));
        Assert.DoesNotThrowAsync(()=>equipmentLogic.CreateEquipmentAsync(equipments, amount));
        
        var response2 = equipmentLogic.CreateEquipmentAsync(equipments, amount);
        Assert.That(response2.Result!,Is.EqualTo(eq));
        Assert.DoesNotThrowAsync(()=>equipmentLogic.CreateEquipmentAsync(equipments, amount));


    }
    
    
    [Test]
    public void CreateEquipmentAsyncTest_E_EmptyEquipmentList()
    {
        EquipmentBasicDto e1 = new EquipmentBasicDto("Golf ball");
        ICollection<EquipmentBasicDto> equipments = new List<EquipmentBasicDto>();
        int amount = 3;
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        equipmentDaoMock.Setup(e => e.CreateEquipmentAsync(equipments, amount)).Returns(Task.FromResult(eq));
        
        var e = Assert.ThrowsAsync<Exception>(() => equipmentLogic.CreateEquipmentAsync(equipments,amount));
        Assert.That(e.Message, Is.EqualTo("Equipment collection is empty"));


    }
    
    [Test]
    public void CreateEquipmentAsyncTest_E_EmptyEquipmentName()
    {
        EquipmentBasicDto e1 = new EquipmentBasicDto("");
        ICollection<EquipmentBasicDto> equipments = new List<EquipmentBasicDto>();
        equipments.Add(e1);
        int amount = 3;
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("");
        eqToList.Add(toAdd);
        equipmentDaoMock.Setup(e => e.CreateEquipmentAsync(equipments, amount)).Returns(Task.FromResult(eq));
        
        var e = Assert.ThrowsAsync<Exception>(() => equipmentLogic.CreateEquipmentAsync(equipments,amount));
        Assert.That(e.Message, Is.EqualTo("Name Field Is Required"));


    }
    
    [Test]
    public void CreateEquipmentAsyncTest_B_EquipmentName50()
    {
        EquipmentBasicDto e1 = new EquipmentBasicDto("Pneumonoultramicroscopicsilicovolcanoconiosisssssss");
        ICollection<EquipmentBasicDto> equipments = new List<EquipmentBasicDto>();
        equipments.Add(e1);
        int amount = 3;
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Pneumonoultramicroscopicsilicovolcanoconiosisssssss");
        eqToList.Add(toAdd);
        equipmentDaoMock.Setup(e => e.CreateEquipmentAsync(equipments, amount)).Returns(Task.FromResult(eq));
        
        var e = Assert.ThrowsAsync<Exception>(() => equipmentLogic.CreateEquipmentAsync(equipments,amount));
        Assert.That(e.Message, Is.EqualTo("Maximum name should be less than 50 characters"));


    }
    
    
    //Tests for GetEquipmentByGameIdAsync in EquipmentLogic
    [Test]
    public void GetEquipmentByGameIdAsync_Z()
    {
        User user = new User("Veronica Tverdohleb", "VeronicaT", "hello", "Member");
        ICollection<User> users = new List<User>();
        users.Add(user);
        Shared.Model.Equipment e1 = new Shared.Model.Equipment("Golf ball");
        ICollection<Shared.Model.Equipment> equipments = new List<Shared.Model.Equipment>();
        //equipments.Add(null);
        int amount = 1;
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
       // eqToList.Add(null);
        eq = eqToList.AsEnumerable();
        
        Shared.Model.Game game = new Shared.Model.Game(null, equipments, users);
        game.Id = 1;
        
      
        gameDaoMock.Setup(g => g.GetGameByIdAsync(game.Id))
            .Returns(Task.FromResult(game)!);
        equipmentDaoMock.Setup(e => e.GetEquipmentByGameIdAsync(game.Id)).Returns(Task.FromResult(eq));
        var response = equipmentLogic.GetEquipmentByGameIdAsync(game.Id);
        Assert.That(response.Result!,Is.EqualTo(eq));
        Assert.DoesNotThrowAsync(()=>equipmentLogic.GetEquipmentByGameIdAsync(game.Id));

       

    }
    
    
    [Test]
    public void GetEquipmentByGameIdAsync_O()
    {
        User user = new User("Veronica Tverdohleb", "VeronicaT", "hello", "Member");
        ICollection<User> users = new List<User>();
        users.Add(user);
        
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
       
        ICollection<Shared.Model.Equipment> equipments = new List<Shared.Model.Equipment>();
        equipments.Add(toAdd);

        eqToList.Add(toAdd);
       
        
        Shared.Model.Game game = new Shared.Model.Game(null, equipments, users);
        
        
      
        gameDaoMock.Setup(g => g.GetGameByIdAsync(game.Id))
            .Returns(Task.FromResult(game)!);
        equipmentDaoMock.Setup(e => e.GetEquipmentByGameIdAsync(game.Id)).Returns(Task.FromResult(eq));
        var response = equipmentLogic.GetEquipmentByGameIdAsync(game.Id).Result;
        Assert.That(equipmentLogic.GetEquipmentByGameIdAsync(game.Id).Result,Is.EqualTo(eq));
        Assert.DoesNotThrowAsync(()=>equipmentLogic.GetEquipmentByGameIdAsync(game.Id));

       

    }
    
    [Test]
    public void GetEquipmentByGameIdAsync_M()
    {
        User user = new User("Veronica Tverdohleb", "VeronicaT", "hello", "Member");
        ICollection<User> users = new List<User>();
        users.Add(user);
        
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        Shared.Model.Equipment toAdd2 = new Shared.Model.Equipment("Golf glove");
       
        ICollection<Shared.Model.Equipment> equipments = new List<Shared.Model.Equipment>();
        equipments.Add(toAdd);
        equipments.Add(toAdd2);

        eqToList.Add(toAdd);
        eqToList.Add(toAdd2);
        eq = eqToList.AsEnumerable();
        
        Shared.Model.Game game = new Shared.Model.Game(null, equipments, users);
        
        
      
        gameDaoMock.Setup(g => g.GetGameByIdAsync(game.Id))
            .Returns(Task.FromResult(game)!);
        equipmentDaoMock.Setup(e => e.GetEquipmentByGameIdAsync(game.Id)).Returns(Task.FromResult(eq));
        var response = equipmentLogic.GetEquipmentByGameIdAsync(game.Id);
        Assert.That(response!.Result,Is.EqualTo(eq));
        Assert.DoesNotThrowAsync(()=>equipmentLogic.GetEquipmentByGameIdAsync(game.Id));

       

    }
    
    [Test]
    public void GetEquipmentByGameIdAsync_E()
    {
        User user = new User("Veronica Tverdohleb", "VeronicaT", "hello", "Member");
        ICollection<User> users = new List<User>();
        users.Add(user);
        
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        Shared.Model.Equipment toAdd2 = new Shared.Model.Equipment("Golf glove");
       
        ICollection<Shared.Model.Equipment> equipments = new List<Shared.Model.Equipment>();
        equipments.Add(toAdd);
        equipments.Add(toAdd2);

        eqToList.Add(toAdd);
        eqToList.Add(toAdd2);
        eq = eqToList.AsEnumerable();
        
        Shared.Model.Game game = new Shared.Model.Game(null, null, users);
        
        
      
        gameDaoMock.Setup(g => g.GetGameByIdAsync(game.Id))
            .Returns(Task.FromResult(game)!);
        equipmentDaoMock.Setup(e => e.GetEquipmentByGameIdAsync(game.Id)).Returns(Task.FromResult(eq));
        var response = Assert.ThrowsAsync<Exception>(() => equipmentLogic.GetEquipmentByGameIdAsync(game.Id));
        Assert.That(response!.Message, Is.EqualTo($"Game with id {game.Id} has no equipments added"));


    }
    
   
    
    //Tests for RentEquipment in EquipmentLogic
    [Test]
    public void RentEquipment_Z()
    {
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        Shared.Model.Equipment toAdd2 = new Shared.Model.Equipment("Golf glove");

        eqToList.Add(toAdd);
        eqToList.Add(toAdd2);
        eq = eqToList.AsEnumerable();
        
        User user = new User("Veronica Tverdohleb", "VeronicaT", "hello", "Member");
        ICollection<User> users = new List<User>();
        users.Add(user);
        
        List<int> equipmentsToBeRented = new List<int>();

        Shared.Model.Game game = new Shared.Model.Game(null, null, users);

        RentEquipmentDto dto = new RentEquipmentDto(game.Id, new List<int>());
        equipmentDaoMock.Setup(e => e.GetAvailableEquipmentAsync())
            .Returns(Task.FromResult<IEnumerable<Shared.Model.Equipment>>(eq));
        gameDaoMock.Setup(g => g.GetGameByIdAsync(game.Id))
            .Returns(Task.FromResult(game)!);
        equipmentDaoMock.Setup(e => e.RentEquipment(dto)).Returns(Task.FromResult(equipmentsToBeRented));
        var response = Assert.ThrowsAsync<Exception>(() => equipmentLogic.RentEquipment(dto));
        Assert.That(response!.Message, Is.EqualTo("Equipments need to be added"));
        
    }
    
    [Test]
    public void RentEquipment_O()
    {
        User user = new User("Veronica Tverdohleb", "VeronicaT", "hello", "Member");
        ICollection<User> users = new List<User>();
        users.Add(user);
        
        List<int> equipmentsToBeRented = new List<int>() {1};
        
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        Shared.Model.Equipment toAdd2 = new Shared.Model.Equipment("Golf glove");

        eqToList.Add(toAdd);
        eqToList.Add(toAdd2);
        eq = eqToList.AsEnumerable();

        Shared.Model.Game game = new Shared.Model.Game(null, null, users);

        RentEquipmentDto dto = new RentEquipmentDto(game.Id, equipmentsToBeRented);
      
        gameDaoMock.Setup(g => g.GetGameByIdAsync(game.Id))
            .Returns(Task.FromResult(game)!);
        equipmentDaoMock.Setup(e => e.GetAvailableEquipmentAsync()).Returns(Task.FromResult(eq));
        equipmentDaoMock.Setup(e => e.RentEquipment(dto)).Returns(Task.FromResult(equipmentsToBeRented));
        Assert.DoesNotThrowAsync(()=>equipmentLogic.RentEquipment(dto));


    }
    
    [Test]
    public void RentEquipment_M()
    {
        User user = new User("Veronica Tverdohleb", "VeronicaT", "hello", "Member");
        ICollection<User> users = new List<User>();
        users.Add(user);
        
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        Shared.Model.Equipment toAdd2 = new Shared.Model.Equipment("Golf glove");

        eqToList.Add(toAdd);
        eqToList.Add(toAdd2);
        eq = eqToList.AsEnumerable();



        List<int> equipmentsToBeRented = new List<int>();// {1,2,3};

        Shared.Model.Game game = new Shared.Model.Game(null, null, users);

        RentEquipmentDto dto = new RentEquipmentDto(game.Id, equipmentsToBeRented);
        foreach (int i in dto.EquipmentIds )
        {
            toAdd = eq.FirstOrDefault(q => q.Id == i);
            equipmentsToBeRented.Add(toAdd.Id);
        }
        gameDaoMock.Setup(g => g.GetGameByIdAsync(game.Id))
            .Returns(Task.FromResult(game)!);
        equipmentDaoMock.Setup(e => e.GetAvailableEquipmentAsync()).Returns(Task.FromResult(eq));

        equipmentDaoMock.Setup(e => e.RentEquipment(dto)).Returns(Task.FromResult(equipmentsToBeRented));
        Assert.DoesNotThrowAsync(()=>equipmentLogic.RentEquipment(dto));


    }
    
    
    [Test]
    public void GetAvailableEquipmentAsync_Z()
    {
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        
         eq = eqToList.AsEnumerable();
        
       
        equipmentDaoMock.Setup(e => e.GetAvailableEquipmentAsync()).Returns(Task.FromResult(eq));
        Assert.That(equipmentLogic.GetAvailableEquipmentAsync().Result,Is.EqualTo(eq));

        Assert.DoesNotThrowAsync(()=>equipmentLogic.GetAvailableEquipmentAsync());


    }
    [Test]
    public void GetAvailableEquipmentAsync_O()
    {
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        Shared.Model.Equipment toAdd2 = new Shared.Model.Equipment("Golf glove");

        eqToList.Add(toAdd);
        eq = eqToList.AsEnumerable();
        
       
        equipmentDaoMock.Setup(e => e.GetAvailableEquipmentAsync()).Returns(Task.FromResult(eq));
        Assert.That(equipmentLogic.GetAvailableEquipmentAsync().Result,Is.EqualTo(eq));
        Assert.DoesNotThrowAsync(()=>equipmentLogic.GetAvailableEquipmentAsync());


    }
    
    [Test]
    public void GetAvailableEquipmentAsync_M()
    {
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        Shared.Model.Equipment toAdd2 = new Shared.Model.Equipment("Golf glove");

        eqToList.Add(toAdd);
        eqToList.Add(toAdd2);
        eq = eqToList.AsEnumerable();
        
       
        equipmentDaoMock.Setup(e => e.GetAvailableEquipmentAsync()).Returns(Task.FromResult(eq));
        Assert.That(equipmentLogic.GetAvailableEquipmentAsync().Result,Is.EqualTo(eq));
        Assert.DoesNotThrowAsync(()=>equipmentLogic.GetAvailableEquipmentAsync());


    }
    
    
      [Test]
    public void GetAllEquipmentAsync_Z()
    {
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        
         eq = eqToList.AsEnumerable();
         SearchEquipmentDto searchEquipmentDto = new SearchEquipmentDto("Golf");
        
       
        equipmentDaoMock.Setup(e => e.GetEquipmentsAsync(searchEquipmentDto)).Returns(Task.FromResult(eq));
        Assert.That(equipmentLogic.GetEquipmentAsync(searchEquipmentDto).Result,Is.EqualTo(eq));

        Assert.DoesNotThrowAsync(()=>equipmentLogic.GetAvailableEquipmentAsync());


    }
    [Test]
    public void GetAllEquipmentAsync_O()
    {
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        Shared.Model.Equipment toAdd2 = new Shared.Model.Equipment("Golf glove");
        eqToList.Add(toAdd);
        eq = eqToList.AsEnumerable();
        SearchEquipmentDto searchEquipmentDto = new SearchEquipmentDto("Golf");
        
       
        equipmentDaoMock.Setup(e => e.GetEquipmentsAsync(searchEquipmentDto)).Returns(Task.FromResult(eq));
        Assert.That(equipmentLogic.GetEquipmentAsync(searchEquipmentDto).Result,Is.EqualTo(eq));
        Assert.DoesNotThrowAsync(()=>equipmentLogic.GetEquipmentAsync(searchEquipmentDto));


    }
    
    [Test]
    public void GetAllEquipmentAsync_M()
    {
        IEnumerable<Shared.Model.Equipment> eq = new  List<Shared.Model.Equipment>();
        List<Shared.Model.Equipment> eqToList = eq.ToList();
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        Shared.Model.Equipment toAdd2 = new Shared.Model.Equipment("Golf glove");
        eqToList.Add(toAdd);
        eqToList.Add(toAdd2);
        eq = eqToList.AsEnumerable();
        SearchEquipmentDto searchEquipmentDto = new SearchEquipmentDto("Golf");
        
       
        equipmentDaoMock.Setup(e => e.GetEquipmentsAsync(searchEquipmentDto)).Returns(Task.FromResult(eq));
        Assert.That(equipmentLogic.GetEquipmentAsync(searchEquipmentDto).Result,Is.EqualTo(eq));
        Assert.DoesNotThrowAsync(()=>equipmentLogic.GetEquipmentAsync(searchEquipmentDto));
        

    }
    [Test]
    public void UpdateEquipmentAsync_Z_Amount0()
    {
        List<Shared.Model.Equipment> equipments = new List<Shared.Model.Equipment>();
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        equipments.Add(toAdd);

        string name = "Golf ball";
        int amount = 0; 
        
        equipmentDaoMock.Setup(e=>e.GetEquipmentListByNameAsync(name)).Returns(Task.FromResult(equipments));
        
        var e = Assert.ThrowsAsync<Exception>(() => equipmentLogic.UpdateEquipmentAsync(name,amount));
        Assert.That(e.Message, Is.EqualTo($"Amount of {name} should be greater than zero."));

        
    }

    [Test]
    public void UpdateEquipmentAsync_O()
    {
        List<Shared.Model.Equipment> equipments = new List<Shared.Model.Equipment>();
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        equipments.Add(toAdd);

        string name = "Golf ball";
        int amount = 1;
        equipmentDaoMock.Setup(e=>e.GetEquipmentListByNameAsync(name)).Returns(Task.FromResult(equipments));
       
        Assert.DoesNotThrowAsync(()=>equipmentLogic.UpdateEquipmentAsync(name,amount));

    }
    
    [Test]
    public void UpdateEquipmentAsync_M()
    {
        List<Shared.Model.Equipment> equipments = new List<Shared.Model.Equipment>();
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        Shared.Model.Equipment toAdd2 = new Shared.Model.Equipment("Golf ball");
        Shared.Model.Equipment toAdd3 = new Shared.Model.Equipment("Golf ball");
        equipments.Add(toAdd);
        equipments.Add(toAdd2);
        equipments.Add(toAdd3);

        string name = "Golf ball";
        int amount = 2;
        equipmentDaoMock.Setup(e=>e.GetEquipmentListByNameAsync(name)).Returns(Task.FromResult(equipments));
       
        Assert.DoesNotThrowAsync(()=>equipmentLogic.UpdateEquipmentAsync(name,amount));

    }
    
    [Test]
    public void UpdateEquipmentAsync_E()
    {
        List<Shared.Model.Equipment> equipments = new List<Shared.Model.Equipment>();
       
        string name = "glove";
        int amount = 2;
        equipmentDaoMock.Setup(e=>e.GetEquipmentListByNameAsync(name)).Returns(Task.FromResult(equipments));
       
        var e = Assert.ThrowsAsync<Exception>(() => equipmentLogic.UpdateEquipmentAsync(name,amount));
        Assert.That(e.Message, Is.EqualTo($"No equipment with name {name} found"));

        
    }
    [Test]
    public void GetEquipmentByNameAsync_O()
    {
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        string name = "Golf ball";

        equipmentDaoMock.Setup(e => e.GetEquipmentByNameAsync(name))
            .Returns(Task.FromResult<Shared.Model.Equipment?>(toAdd));

        var resposne = equipmentLogic.GetEquipmentByNameAsync(name);
        Assert.That(resposne.Result.Name!,Is.EqualTo(toAdd.Name));
        Assert.DoesNotThrowAsync(()=>equipmentLogic.GetEquipmentByNameAsync(name));

    }
    
    [Test]  // This is both Zero and Exception scenario
    public void GetEquipmentByNameAsync_E()
    {
        string name = "Golf ball";
        equipmentDaoMock.Setup(e => e.GetEquipmentByNameAsync(name))
            .Returns(Task.FromResult<Shared.Model.Equipment?>(null));

        var e = Assert.ThrowsAsync<Exception>(() => equipmentLogic.GetEquipmentByNameAsync(name));
        Assert.That(e.Message, Is.EqualTo($"Equipment with name {name} not found"));
    }

    [Test]
    public void GetEquipmentByIdAsync_O()
    {
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");

        equipmentDaoMock.Setup(e => e.GetEquipmentByIdAsync(1))
            .Returns(Task.FromResult<Shared.Model.Equipment?>(toAdd));

        var resposne = equipmentLogic.GetEquipmentByIdAsync(1);
        Assert.That(resposne.Result.Name!,Is.EqualTo(toAdd.Name));
        Assert.DoesNotThrowAsync(()=>equipmentLogic.GetEquipmentByIdAsync(1));

    }
    
    [Test]  // This is both Zero and Exception scenario
    public void GetEquipmentByIdAsync_E()
    {
        equipmentDaoMock.Setup(e => e.GetEquipmentByIdAsync(1))
            .Returns(Task.FromResult<Shared.Model.Equipment?>(null));

        var e = Assert.ThrowsAsync<Exception>(() => equipmentLogic.GetEquipmentByIdAsync(1));
        Assert.That(e.Message, Is.EqualTo($"Equipment with id 1 not found"));
    }

    
    
    [Test]
    public void DeleteEquipmentAsync_Z()
    {
        List<Shared.Model.Equipment> found = new List<Shared.Model.Equipment>();
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        //found.Add(toAdd);
        string name = "ball";

        //equipmentDaoMock.Setup(e => e.DeleteEquipmentAsync("Golf ball")).Returns(Task.FromResult(found));
        equipmentDaoMock.Setup(e => e.GetEquipmentListByNameAsync(name))
            .Returns(Task.FromResult(found));

        var e = Assert.ThrowsAsync<Exception>(() => equipmentLogic.DeleteEquipmentAsync(name));
        Assert.That(e.Message, Is.EqualTo("No equipment with such name exist"));

        
       // Assert.DoesNotThrowAsync(()=>equipmentLogic.DeleteEquipmentAsync(name));
        
    }
    
    [Test]
    public void DeleteEquipmentAsync_O()
    {
        List<Shared.Model.Equipment> found = new List<Shared.Model.Equipment>();
        Shared.Model.Equipment toAdd = new Shared.Model.Equipment("Golf ball");
        found.Add(toAdd);
        string name = "Golf ball";

        //equipmentDaoMock.Setup(e => e.DeleteEquipmentAsync("Golf ball")).Returns(Task.FromResult(found));
        equipmentDaoMock.Setup(e => e.GetEquipmentListByNameAsync(name))
            .Returns(Task.FromResult(found));

        //Assert.DoesNotThrowAsync(()=>equipmentLogic.DeleteEquipmentAsync(name));
        
    }


    
    
    
}