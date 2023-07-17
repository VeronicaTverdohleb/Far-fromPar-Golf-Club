using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace DataAccess.DAOs;

public class EquipmentDao: IEquipmentDao
{
    private readonly DataContext context;

    public EquipmentDao(DataContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Equipment>> CreateEquipmentAsync(IEnumerable<EquipmentBasicDto>  equipment, int amount)//call in the page
    {
        List<Equipment> addedEquipment = new List<Equipment>();

        foreach (var equipmentItem in equipment)
        {
            for (int i = 0; i <= amount; i++)
            {

                Equipment newEquipment = new Equipment(equipmentItem.Name)
                {
                    Name = equipmentItem.Name

                };


                EntityEntry<Equipment> added = await context.Equipments.AddAsync(newEquipment);
               

                addedEquipment.Add(added.Entity);
                await context.SaveChangesAsync();
            }
        }

        return addedEquipment;
       
       
    }

    public async Task UpdateEquipmentAsync(string? name, int amount)
    {
        /*  var entriesToDelete = context.Equipments.Where(e => e.Name == name).Take(amount).ToList();
        foreach (var entry in entriesToDelete)
        {
            context.Equipments.Remove(entry);
        }
        await context.SaveChangesAsync();
      
        var entryToDelete = context.Equipments.FirstOrDefault(e => e.Name == name);
        if (entryToDelete != null)
        {
            context.Equipments.Remove(entryToDelete);
            await context.SaveChangesAsync();
        }*/
        var entryToDelete = context.Equipments.FirstOrDefault(e => e.Name == name);
        if (entryToDelete != null)
        {
            context.Equipments.Remove(entryToDelete);
            await context.SaveChangesAsync();
        }
    }

    public Task<IEnumerable<Equipment>> GetEquipmentsAsync(SearchEquipmentDto searchParameters)
    {
      IEnumerable<Equipment> list = context.Equipments.ToList();
      if (!string.IsNullOrEmpty(searchParameters.NameContains))
      {
          list = list.Where(e => e.Name.Contains(searchParameters.NameContains, StringComparison.OrdinalIgnoreCase));
      }
        return Task.FromResult(list);
    }

    public async Task<Equipment?> GetEquipmentByNameAsync(string name)
    {
        var found = await context.Equipments
            .AsNoTracking().SingleOrDefaultAsync(e => e.Name == name);
        return found;
       
    }

    public int GetCountOfEquipment(string name)
    {
        IQueryable<Equipment> q = context.Equipments.AsQueryable();
        int count = 0;
        if (name != null)
        {
            count = q.Where(e=>e.Name==name).GroupBy(e => e.Name).Count();
                
        }

        return count;
    }

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

    public async Task<List<Equipment>> GetEquipmentListByNameAsync(string name)
    {
        return await context.Equipments.Where(e => e.Name.Equals(name)).ToListAsync();
    }

    public async Task DeleteEquipmentAsync(string? name)
    {
        var entriesToDelete = context.Equipments.Where(e => e.Name == name).ToList();
        foreach (var entry in entriesToDelete)
        {
            context.Equipments.Remove(entry);
        }

        await context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Equipment>> GetAvailbaleEquipment()
    {
        var result = await context.Equipments.Where(e=>!context.Games.Any(g=>g.Equipments.Contains(e))).ToListAsync();

        return result;
        //  IQueryable<Equipment> equipments = context.Equipments.AsQueryable();
           
         
        // equipments =  "SELECT * FROM Equipments WHERE Id NOT IN (SELECT EquipmentsId FROM EquipmentGame)";
        //IEnumerable<Equipment> result = await query.ToList();
           
        //return result;


    }

    public async Task RentEquipment(RentEquipmentDto dto )
    {
        Game? existing = await context.Games.FirstOrDefaultAsync(g => g.Id == dto.GameId);
        List<Equipment> forRentEquipment = new List<Equipment>();
        var availableEquipment = await GetAvailbaleEquipment();
        foreach (int equipment in dto.EquipmentIds )
        {
            Equipment? e = availableEquipment.FirstOrDefault(eq => eq.Id == equipment);
            if (e != null)
            {
                forRentEquipment.Add(e);
            }
            forRentEquipment.Add(e);
        }
        foreach (var equipment in forRentEquipment)
        {
            existing.Equipments.Add(equipment);
        }

       // existing.Equipments = forRentEquipment;
      //  context.Games.Update(existing);
        await context.SaveChangesAsync();
        // Equipment? equipment = await context.Equipments.FirstOrDefault(e=>e.Id.Equals(dto.EquipmentIds))

        /* List<Equipment> forRentEquipment = new List<Equipment>();
           var availableEquipment = await GetAvailbaleEquipment();
           foreach (int equipment in dto.EquipmentIds )
           {
               Equipment? e = availableEquipment.FirstOrDefault(eq => eq.Id == equipment);
               if (e != null)
               {
                   forRentEquipment.Add(e);
               }
               forRentEquipment.Add(e);
           }
   
           Game newGame = new Game(dto.GameId, forRentEquipment);
           EntityEntry<Game> added = await context.Games.AddAsync(newGame);
           await context.SaveChangesAsync();
           return added.Entity;*/


    }
    /*List<string> deletedNames = new List<string>();

    
        IEnumerable<Equipment> existingList = await GetEquipmentListByNameAsync(name);

        foreach (Equipment existing in existingList)
        {
            context.Equipments.Remove(existing);
            deletedNames.Add(existing.Name);
        }
        
    

    await context.SaveChangesAsync();

    return deletedNames;*/
    /* public async  Task<IEnumerable<string>> DeleteEquipmentAsync(string name, int amount)
     {
         List<Equipment> existingList = await GetEquipmentListByNameAsync(name);
     
         if (existingList.Count == 0)
         {
             throw new Exception($"Equipment with name {name} not found");
         }
         int deleteCount = Math.Min(existingList.Count, amount);
         List<string> deletedNames = new List<string>();
 
         for (int i = 0; i < deleteCount; i++)
         {
             Equipment existing = existingList[i];
             context.Equipments.Remove(existing);
             await context.SaveChangesAsync();
             deletedNames.Add(existing.Name);
             Console.WriteLine("in dao");
         }
 
         await context.SaveChangesAsync();
 
         return deletedNames;
         }*/


        /*Equipment? existing = await GetEquipmentByNameAsync(name);
        while (existing != null && existing.Name.Equals(name))
        {
            context.Equipments.Remove(existing);  
            await context.SaveChangesAsync();
        }
        if (existing == null)
        {
            throw new Exception($"Equipment with name {name} not found");
        }*/


        

}