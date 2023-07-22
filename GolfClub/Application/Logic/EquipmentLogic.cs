using System.Collections;
using System.Diagnostics;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos.EquipmentDto;
using Shared.Dtos.GameDto;
using Shared.Model;

namespace Application.Logic;

public class EquipmentLogic: IEquipmentLogic
{
    private readonly IEquipmentDao equipmentDao;
    private readonly IGameDao gameDao;
    private readonly IUserDao userDao;


    public EquipmentLogic(IEquipmentDao equipmentDao, IGameDao gameDao)
    {
        this.equipmentDao = equipmentDao;
        this.gameDao = gameDao;
    }

    public async Task<IEnumerable<Equipment>> CreateEquipmentAsync(IEnumerable<EquipmentBasicDto>  equipment, int amount)
    {
        if (equipment == null || !equipment.Any())
            {
                throw new Exception("Equipment collection is empty");
            }

            foreach (var equipmentItem in equipment)
            {
                if (string.IsNullOrEmpty(equipmentItem.Name))
                {
                    throw new Exception("Name Field Is Required");
                }
                if (equipmentItem.Name.Length > 50)
                {
                    throw new Exception("Max Name Length Is 50 Characters");
                }
            }

            IEnumerable<Equipment> created = await equipmentDao.CreateEquipmentAsync(equipment, amount);

            return created;

        
       
        
      
    }

    public async Task UpdateEquipmentAsync(string name, int amount)
    {
      
        for (int i = 0; i < amount; i++)

        {
            List<Equipment> findEquipment = await equipmentDao.GetEquipmentListByNameAsync(name);

            Equipment equipmentToDelete = findEquipment.First();

            await equipmentDao.UpdateEquipmentAsync(equipmentToDelete.Name, amount);
            
        }/*  List<Equipment> findEquipment = await equipmentDao.GetEquipmentListByNameAsync(name);
        var entriesToDelete = findEquipment.Take(amount).ToList();
        foreach (var entry in entriesToDelete)
        {
            await equipmentDao.DeleteEquipmentAsync(entry.Name);
        }*/

       
        
    }


    public Task<IEnumerable<Equipment>> GetEquipmentAsync(SearchEquipmentDto searchParameters)
    {
        return equipmentDao.GetEquipmentsAsync(searchParameters);
    }

    public async Task<Equipment?> GetEquipmentByIdAsync(int id)
    {
        Equipment? findEquipment = await equipmentDao.GetEquipmentByIdAsync(id);
        if (findEquipment == null)
        {
            throw new Exception($"Equipment with id {id} not found");
        }

        return new Equipment(findEquipment.Name); 

    }

    public async Task<Equipment?> GetEquipmentByNameAsync(string name)
    {
        Equipment? findEquipment= await equipmentDao.GetEquipmentByNameAsync(name);
        if (findEquipment == null)
        {
            throw new Exception($"Equipment with name {name} not found");
        }

        return new Equipment(findEquipment.Name); 
        
    }
    public Task<List<Equipment>> GetEquipmentListAsync( string name)
    {
        return equipmentDao.GetEquipmentListByNameAsync(name);
    }

    public async Task RentEquipment(RentEquipmentDto dto)
    {
            IEnumerable<Equipment> availableEquipment = await GetAvailableEquipmentAsync();
            List<int>? forRentEquipment = new List<int>();
            Game? game = await gameDao.GetGameByIdAsync(dto.GameId);
           

            foreach (int equipmentName in dto.EquipmentIds)
            {
                Equipment equipment = availableEquipment.FirstOrDefault(eq => eq.Id == equipmentName);

                if (equipment != null)
                {
                    forRentEquipment.Add(equipment.Id);
                }
            }

            RentEquipmentDto newRented = new RentEquipmentDto(game.Id, forRentEquipment);
          
            
            await equipmentDao.RentEquipment(newRented);
        
    }

    public async Task DeleteEquipmentAsync(string name)
    {

        while (true)
            {
                List<Equipment> findEquipment = await equipmentDao.GetEquipmentListByNameAsync(name);
                if (findEquipment.Count== 0)
                {
                    break; // Exit the loop if no more equipment with the given name is found
                }
                foreach (Equipment equipment in findEquipment)
                {
                    await equipmentDao.DeleteEquipmentAsync(equipment.Name);
                }
               
                
                Console.WriteLine("in logic");
            }
        
 
    }
/*IEnumerable<Equipment> allEquipments = await equipmentDao.GetEquipmentByGameIdAsync(gameId);
        foreach (var equipment in allEquipments)
        {
            equipment.Games.Clear(); // Assuming you have a collection of games in Equipment entity
        }*/
    public async Task DeleteAllEquipmentByGameIdAsync(int gameId)
    {
        
        
        
       /* IEnumerable<Equipment> equipments = await GetEquipmentByGameIdAsync(gameId);
      //  List<Equipment>? forRentEquipment = new List<Equipment>();
        Game? game = await gameDao.GetGameByIdAsync(gameId);
           
        IEnumerable<Equipment> forRentEquipment = equipments.Except(game.Equipments);

        foreach (var equipmentName in game.Equipments)
        {
            Equipment equipment = equipments.FirstOrDefault(eq => eq.Id == equipmentName.Id);
            forRentEquipment = equipments.ToList();

            if (equipment != null)
            {
                forRentEquipment.Remove(equipment);
            }
        }*/
       Debug.WriteLine($"Deleting equipment for Game ID: {gameId}");

        await equipmentDao.DeleteAllEquipmentByGameIdAsync(gameId);

    }
    public Task<IEnumerable<Equipment>> GetAvailableEquipmentAsync()
    {
        return equipmentDao.GetAvailableEquipmentAsync();
    }

    public async Task<IEnumerable<Equipment>> GetEquipmentByGameIdAsync(int gameId)
    {
        Game? game = await gameDao.GetGameByIdAsync(gameId);
        if (game == null)
        {
            throw new Exception($"Game with id {gameId} not found");

        }

        IEnumerable<Equipment> eInGame = await equipmentDao.GetEquipmentByGameIdAsync(game.Id);

        return eInGame;
    }

    /* public async Task<List<int>> GetAvailableEquipmentIds()
    {
        return await equipmentDao.GetAvailableEquipmentIds();
    }

    public async Task<List<int>> GetGameEquipmentIds(int gameId)
    {
        return await equipmentDao.GetGameEquipmentIds(gameId);
    }*/


    /*public async Task<Equipment> RentEquipment(RentEquipmentDto dto)
    {
        foreach (string eq in dto.EquipmentNames)
        {
            Equipment? existing = await equipmentDao.GetEquipmentByNameAsync(eq);
            if (existing is { Name: null })
            {
                throw new Exception($"This equipment you try to use, does not exist!");

            }

            if (dto.EquipmentNames == null || !dto.EquipmentNames.Any())
            {
                throw new Exception("You need to select equipments in order to rent");

            }
        }
        
          }*/
}