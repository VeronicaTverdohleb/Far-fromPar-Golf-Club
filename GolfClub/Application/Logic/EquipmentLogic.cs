using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace Application.Logic;

public class EquipmentLogic: IEquipmentLogic
{
    private readonly IEquipmentDao equipmentDao;


    public EquipmentLogic(IEquipmentDao equipmentDao)
    {
        this.equipmentDao = equipmentDao;
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