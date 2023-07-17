using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IEquipmentLogic
{
    Task<IEnumerable<Equipment>> CreateEquipmentAsync(IEnumerable<EquipmentBasicDto>  equipment, int amount);
    Task UpdateEquipmentAsync(string name, int amount);
    Task<IEnumerable<Equipment>> GetEquipmentAsync(SearchEquipmentDto searchParameters);
    Task<Equipment?> GetEquipmentByIdAsync(int id);
    Task<Equipment?> GetEquipmentByNameAsync(string name);
    Task DeleteEquipmentAsync(string name);
    Task<List<Equipment>> GetEquipmentListAsync(string name);
    Task RentEquipment(RentEquipmentDto dto, string username);
    Task<IEnumerable<Equipment>> GetAvailableEquipment();
}