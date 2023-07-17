using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace Application.DaoInterfaces;

public interface IEquipmentDao
{
    Task<IEnumerable<Equipment>> CreateEquipmentAsync(IEnumerable<EquipmentBasicDto>  equipment, int amount);
    Task UpdateEquipmentAsync(string? name, int amount);
    Task<IEnumerable<Equipment>> GetEquipmentsAsync(SearchEquipmentDto searchParameters);
    Task<Equipment?> GetEquipmentByNameAsync(string name);
    Task<Equipment?> GetEquipmentByIdAsync(int id);
    Task DeleteEquipmentAsync(string? name);
    Task<List<Equipment>> GetEquipmentListByNameAsync(string name);
    Task RentEquipment(RentEquipmentDto dto);
    Task<IEnumerable<Equipment>> GetAvailbaleEquipment();
}