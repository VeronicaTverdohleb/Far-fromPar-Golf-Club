using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IEquipmentLogic
{
    Task<Equipment> CreateEquipmentAsync(Equipment equipment);
    Task UpdateEquipmentAmount(EquipmentBasicDto dto);
    Task<IEnumerable<Equipment>> GetEquipmentAsync();
    Task<Equipment?> GetEquipmentByIdAsync(int id);
    Task<Equipment?> GetEquipmentByNameAsync(string name);
    Task DeleteEquipmentAsync(IEnumerable<string> name);
}