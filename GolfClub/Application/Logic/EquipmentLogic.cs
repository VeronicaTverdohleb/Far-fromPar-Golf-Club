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

    public async Task<Equipment> CreateEquipmentAsync(Equipment equipment)
    {
        Equipment? equipment1 = await equipmentDao.GetEquipmentByNameAsync(equipment.Name);
       
        if (string.IsNullOrEmpty(equipment.Name))
        {
            throw new Exception("Name Field Is Required");
        }
        if (equipment.Name.Length > 50)
        {
            throw new Exception("Max Name Length Is 50 Characters");
        }

        Equipment eq = new Equipment(equipment.Name);
        Equipment created = await equipmentDao.CreateEquipmentAsync(eq);
        
        
        return created;
    }

    public async Task UpdateEquipmentAmount(EquipmentBasicDto dto)
    {
        Equipment? equipment = await equipmentDao.GetEquipmentByIdAsync(dto.Id);
        if (equipment == null)
        {
            throw new Exception($"Equipment with the id {dto.Id} was not found!");
        }

        int amountToUse = dto.Amount;
        Equipment updated = new(equipment.Name)
        {
            Id = equipment.Id
        };

        await equipmentDao.UpdateEquipmentAsync(updated); 
    }

    public Task<IEnumerable<Equipment>> GetEquipmentAsync()
    {
        return equipmentDao.GetEquipmentsAsync();
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

    public async Task DeleteEquipmentAsync(IEnumerable<string> names)
    {
        foreach (var name in names)
        {
            Equipment? findEquipment = await equipmentDao.GetEquipmentByNameAsync(name);
            if (findEquipment == null)
            {
                throw new Exception($"Equipment with name {name} was not found!");
            } 
        }
        
        
        await equipmentDao.DeleteEquipmentAsync(names);
    }
}