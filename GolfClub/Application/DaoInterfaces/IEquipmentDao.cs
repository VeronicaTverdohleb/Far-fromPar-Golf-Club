using Shared.Model;

namespace Application.DaoInterfaces;

public interface IEquipmentDao
{
    Task<Equipment> CreateEquipmentAsync(Equipment equipment);
    Task UpdateEquipmentAsync(Equipment equipment);
    Task<IEnumerable<Equipment>> GetEquipmentsAsync();
    Task<Equipment?> GetEquipmentByNameAsync(string name);
    Task<Equipment?> GetEquipmentByIdAsync(int id);
    Task DeleteEquipmentAsync(IEnumerable<string> name);
}