using System.Diagnostics;
using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace DataAccess.DAOs;

/// <summary>
/// Data Access Object responsible for interacting with the database
/// Used in EquipmentDao
/// </summary>
public class EquipmentDao : IEquipmentDao
{
    private readonly DataContext context;

    public EquipmentDao(DataContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Method which inserts new equipment in the database
    /// </summary>
    /// <param name="equipment"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Equipment>> CreateEquipmentAsync(IEnumerable<EquipmentBasicDto> equipment, int amount)
    {
        List<Equipment> addedEquipment = new List<Equipment>(); //Creating new list of Equipments

        foreach (var equipmentItem in equipment) //Iterating through each EquipmentBasicDto object in the list
        {
            for (int i = 0; i <= amount; i++)
            {
                Equipment newEquipment = new Equipment(equipmentItem.Name) //Creating new instances
                {
                    Name = equipmentItem.Name //and initializing with "Name"
                };

                //Adding the new Equipment object to the database
                EntityEntry<Equipment> added = await context.Equipments.AddAsync(newEquipment);

                //Adding the Equipment object to the "addedEquipment" list
                addedEquipment.Add(added.Entity);
                await context.SaveChangesAsync();
            }
        }

        return addedEquipment;
    }

    /// <summary>
    /// Method which removes  from the database the first instance of with the matching equipment name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="amount"></param>
    public async Task UpdateEquipmentAsync(string? name, int amount)
    {
        var entryToDelete = context.Equipments.FirstOrDefault(e => e.Name == name);
        if (entryToDelete != null)
        {
            context.Equipments.Remove(entryToDelete);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    ///Method which fetches equipments from the database based on search parameters
    /// </summary>
    /// <param name="searchParameters"></param>
    /// <returns></returns>
    public Task<IEnumerable<Equipment>> GetEquipmentsAsync(SearchEquipmentDto searchParameters)
    {
        IEnumerable<Equipment> list = context.Equipments.ToList();
        if (!string.IsNullOrEmpty(searchParameters.NameContains))
        {
            list = list.Where(e => e.Name.Contains(searchParameters.NameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(list);
    }

    /// <summary>
    /// Method which fetches equipments from the database based on name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<Equipment?> GetEquipmentByNameAsync(string name)
    {
        var found = await context.Equipments
            .AsNoTracking().SingleOrDefaultAsync(e => e.Name == name);
        return found;
    }

    /// <summary>
    /// Method which fetches equipments from the database based on id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Equipment?> GetEquipmentByIdAsync(int id)
    {
        Equipment? found = await context.Equipments.AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == id);
        if (found == null)
        {
            throw new Exception($"Equipment with id {id} not found");
        }

        return found;
    }

    /// <summary>
    /// Method which fetches equipment in for of a list from the database based on name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<List<Equipment>> GetEquipmentListByNameAsync(string name)
    {
        return await context.Equipments.Where(e => e.Name.Equals(name)).ToListAsync();
    }

    /// <summary>
    /// Method which removes all equipments from the database which match the name 
    /// </summary>
    /// <param name="name"></param>
    public async Task DeleteEquipmentAsync(string? name)
    {
        var entriesToDelete = context.Equipments.Where(e => e.Name == name).ToList();
        foreach (var entry in entriesToDelete)
        {
            context.Equipments.Remove(entry);
        }

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Method which updates the Game context and the database by removing all the equipment instances
    /// based on the game id
    /// </summary>
    /// <param name="dto"></param>
    public async Task DeleteAllEquipmentByGameIdAsync(RentEquipmentDto dto)
    {
        Debug.WriteLine($"Deleting equipment for Game ID: {dto.GameId}");
        Game? existing = await context.Games.FirstOrDefaultAsync(g => g.Id == dto.GameId);
        existing!.Equipments!.Clear(); // Clear all existing equipment associations for the game
        context.Games.Update(existing);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Method fetches all the equipments from the database in form of a list which are not associated with a game
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Equipment>> GetAvailableEquipmentAsync()
    {
        IEnumerable<Equipment> result = context.Equipments.Where(e => !context.Games.Any(g => g.Equipments.Contains(e)))
            .AsEnumerable();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Method which fetches all the equipments from the database in form of a list which are associated with a game
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
    public Task<IEnumerable<Equipment>> GetEquipmentByGameIdAsync(int gameId)
    {
        IEnumerable<Equipment> result = context.Equipments
            .Where(e => context.Games.Any(g => g.Equipments.Contains(e) && g.Id == gameId))
            .AsEnumerable();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Method which adds equipment id and game id  to the "EquipmentGame" joined table in the database
    /// </summary>
    /// <param name="dto"></param>
    /// <exception cref="Exception"></exception>
    public async Task RentEquipment(RentEquipmentDto dto)
    {
        //Fetching game with the specified "dto.GameId"
        Game? existing = await context.Games.FirstOrDefaultAsync(g => g.Id == dto.GameId);
        if (existing == null)
        {
            throw new Exception("Game not found with the specified ID.");
        }

        //Creating an empty list 
        List<Equipment> forRentEquipment = new List<Equipment>();
        //Method call
        var availableEquipment = await GetAvailableEquipmentAsync();
        //Iterating through the "dto.EquipmentIds" list
        foreach (int equipment in dto.EquipmentIds)
        {
            //Fetching equipment from the "availableEquipment" list which match the id
            Equipment? e = availableEquipment.FirstOrDefault(eq => eq.Id == equipment);
            if (e != null)
            {
                //Adding to the "forRentEquipment" list
                forRentEquipment.Add(e);
            }
        }

        foreach (var equipment in forRentEquipment)
        {
            existing.Equipments.Add(equipment);
        }

        await context.SaveChangesAsync();
    }
}