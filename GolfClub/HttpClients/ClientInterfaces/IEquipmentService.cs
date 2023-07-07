using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IEquipmentService
{
    Task<ICollection<Equipment>> getAllEquipmentAsync(string? name);
    Task CreateEquipmentAsync(EquipmentBasicDto dto);
    Task UpdateEquipmentAmount(EquipmentBasicDto dto);
    Task<Equipment?> GetEquipmentByNameAsync(string name);
    Task DeleteEquipmentAsync(string name);

    Task<int> CountOfEquipment(string name);
   
}