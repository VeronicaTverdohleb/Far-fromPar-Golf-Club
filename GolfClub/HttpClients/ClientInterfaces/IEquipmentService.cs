using System.Collections;
using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IEquipmentService
{
   public Task<ICollection<Equipment?>> getAllEquipmentAsync(string? name);
   public Task CreateEquipmentAsync(IEnumerable<EquipmentBasicDto>  equipment, int amount);
   public Task UpdateEquipmentAmount(string name, int amount);
   public Task DeleteEquipmentAsync(string? name);
   public Task RentEquipment(RentEquipmentDto dto);
   public Task<ICollection<Equipment>?> GetAvailableEquipment();
   public Task<ICollection<Equipment>?> GetEquipmentByGameIdAsync(int gameId);
   public Task DeleteAllEquipmentByGameIdAsync(RentEquipmentDto dto);


}