using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace Application.Logic;

/// <summary>
/// This class takes care of the business logic for Equipment-related responsibilities
/// </summary>
public class EquipmentLogic : IEquipmentLogic
{
    private readonly IEquipmentDao equipmentDao;
    private readonly IGameDao gameDao;


    /// <summary>
    /// Constructor for EquipmentLogic
    /// </summary>
    /// <param name="equipmentDao"></param>
    /// <param name="gameDao"></param>
    public EquipmentLogic(IEquipmentDao equipmentDao, IGameDao gameDao)
    {
        this.equipmentDao = equipmentDao;
        this.gameDao = gameDao;
    }

    /// <summary>
    /// This method is the business logic for creating an equipment.
    /// It takes a list of equipments and an amount
    /// Throws exceptions if the equipment list is empty, the name field is empty and if the name has more than 50 characters
    /// </summary>
    /// <param name="equipment"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// This method takes the equipment name and the amount and deletes that amount of equipment
    /// Throws exceptions whenever the amount is 0 and if the equipment with the requested name does not exist
    /// </summary>
    /// <param name="name"></param>
    /// <param name="amount"></param>
    /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// Gets all the equipment based on the search parameter, which is name
    /// </summary>
    /// <param name="searchParameters"></param>
    /// <returns></returns>
    public Task<IEnumerable<Equipment>> GetEquipmentAsync(SearchEquipmentDto searchParameters)
    {
        return equipmentDao.GetEquipmentsAsync(searchParameters);
    }

    /// <summary>
    /// Gets all equipments based on the id
    /// Throws exception if no equipment with such id found
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Equipment?> GetEquipmentByIdAsync(int id)
    {
        Equipment? findEquipment = await equipmentDao.GetEquipmentByIdAsync(id);
        if (findEquipment == null)
        {
            throw new Exception($"Equipment with id {id} not found");
        }

        return new Equipment(findEquipment.Name);
    }

    /// <summary>
    /// Gets all the equipment by the name
    /// Throws exception if no equipment with such name found
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Equipment?> GetEquipmentByNameAsync(string name)
    {
        Equipment? findEquipment = await equipmentDao.GetEquipmentByNameAsync(name);
        if (findEquipment == null)
        {
            throw new Exception($"Equipment with name {name} not found");
        }

        return new Equipment(findEquipment.Name);
    }

    /// <summary>
    /// This method is the business logic for renting equipment.
    /// Takes the game id and a list of equipment ids from the dto
    /// Throws exceptions if no game with the specified id found and if the equipment list is empty
    /// </summary>
    /// <param name="dto"></param>
    /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// This method is the business logic for deleting equipment.
    /// This method deletes all equipments based on the provided name
    /// Throws exception if no equipment with such name exists
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="Exception"></exception>
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

    /// <summary>
    /// This method deletes all the equipments associated with a game id
    /// Throws exception if dto is null
    /// </summary>
    /// <param name="dto"></param>
    /// <exception cref="Exception"></exception>
    public async Task DeleteAllEquipmentByGameIdAsync(RentEquipmentDto dto)
    {
        if (dto == null)
        {
            throw new Exception("Dto cannot be null");
        }

        Game? game = await gameDao.GetGameByIdAsync(dto.GameId);
        await equipmentDao.DeleteAllEquipmentByGameIdAsync(dto);
    }

    /// <summary>
    ///This method gets all the equipment which is not associated with any game id
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Equipment>> GetAvailableEquipmentAsync()
    {
        return equipmentDao.GetAvailableEquipmentAsync();
    }

    /// <summary>
    /// This method gets the equipment associated with a game id
    /// Throws exceptions if no game with such id found and if that game does not have equipments
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
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