using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IEquipmentLogic
{
    public Task<IEnumerable<Equipment>> CreateEquipmentAsync(IEnumerable<EquipmentBasicDto> equipment, int amount);
    public Task UpdateEquipmentAsync(string name, int amount);
    public Task<IEnumerable<Equipment>> GetEquipmentAsync(SearchEquipmentDto searchParameters);
    public Task<Equipment?> GetEquipmentByIdAsync(int id);
    public Task<Equipment?> GetEquipmentByNameAsync(string name);
    public Task DeleteEquipmentAsync(string name);
    public Task RentEquipment(RentEquipmentDto dto);
    public Task<IEnumerable<Equipment>> GetAvailableEquipmentAsync();
    public Task<IEnumerable<Equipment>> GetEquipmentByGameIdAsync(int gameId);
    public Task DeleteAllEquipmentByGameIdAsync(RentEquipmentDto dto);
}