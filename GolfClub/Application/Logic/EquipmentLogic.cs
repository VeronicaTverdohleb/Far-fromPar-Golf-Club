using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace Application.Logic;

public class EquipmentLogic : IEquipmentLogic
{
    private readonly IEquipmentDao equipmentDao;
    private readonly IGameDao gameDao;


    public EquipmentLogic(IEquipmentDao equipmentDao, IGameDao gameDao)
    {
        this.equipmentDao = equipmentDao;
        this.gameDao = gameDao;
    }

    public async Task<IEnumerable<Equipment>> CreateEquipmentAsync(IEnumerable<EquipmentBasicDto> equipment, int amount)
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
                throw new Exception("Maximum name should be less than 50 characters");
            }
        }

        IEnumerable<Equipment> created = await equipmentDao.CreateEquipmentAsync(equipment, amount);

        return created;
    }

    public async Task UpdateEquipmentAsync(string name, int amount)
    {
        if (amount <= 0)
        {
            throw new Exception($"Amount of {name} should be greater than zero.");
        }

        for (int i = 0; i < amount; i++)

        {
            List<Equipment> findEquipment = await equipmentDao.GetEquipmentListByNameAsync(name);
            if (findEquipment.Count == 0)
            {
                throw new Exception($"No equipment with name {name} found");
            }

            Equipment equipmentToDelete = findEquipment.First();

            await equipmentDao.UpdateEquipmentAsync(equipmentToDelete.Name, amount);
        }
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
        Equipment? findEquipment = await equipmentDao.GetEquipmentByNameAsync(name);
        if (findEquipment == null)
        {
            throw new Exception($"Equipment with name {name} not found");
        }

        return new Equipment(findEquipment.Name);
    }


    public async Task RentEquipment(RentEquipmentDto dto)
    {
        IEnumerable<Equipment> availableEquipment = await GetAvailableEquipmentAsync();
        List<int>? forRentEquipment = new List<int>();
        Game? game = await gameDao.GetGameByIdAsync(dto.GameId);
        if (game == null)
        {
            throw new Exception($"Game with id {dto.GameId} not found.");
        }

        foreach (int equipmentName in dto.EquipmentIds!)
        {
            Equipment equipment = availableEquipment.FirstOrDefault(eq => eq.Id == equipmentName)!;
            if (equipment == null)
            {
                throw new Exception("Equipments need to be added");
            }

            forRentEquipment.Add(equipment.Id);
        }

        RentEquipmentDto newRented = new RentEquipmentDto(game.Id, forRentEquipment);
        await equipmentDao.RentEquipment(newRented);
    }

    public async Task DeleteEquipmentAsync(string name)
    {
        List<Equipment> findEquipment = await equipmentDao.GetEquipmentListByNameAsync(name);

        if (findEquipment == null || findEquipment.Count == 0)
        {
            throw new Exception("No equipments with such name exist");
        }

        for (int i = 0; i < findEquipment.Count; i++)
        {
            await equipmentDao.DeleteEquipmentAsync(name);
        }
    }

    public async Task DeleteAllEquipmentByGameIdAsync(RentEquipmentDto dto)
    {
        if (dto == null)
        {
            throw new Exception("Dto cannot be null");
        }

        Game? game = await gameDao.GetGameByIdAsync(dto.GameId);
        await equipmentDao.DeleteAllEquipmentByGameIdAsync(dto);
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

        if (game.Equipments == null)
        {
            throw new Exception($"Game with id {gameId} has no equipments added");
        }

        IEnumerable<Equipment> eInGame = await equipmentDao.GetEquipmentByGameIdAsync(game.Id);

        return eInGame;
    }
}