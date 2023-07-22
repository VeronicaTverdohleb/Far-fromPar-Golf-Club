using Shared.Dtos.EquipmentDto;
using Shared.Model;

namespace Application.DaoInterfaces;

public interface IEquipmentDao
{
    public Task<IEnumerable<Equipment>> CreateEquipmentAsync(IEnumerable<EquipmentBasicDto>  equipment, int amount);
    public Task UpdateEquipmentAsync(string? name, int amount);
    public Task<IEnumerable<Equipment>> GetEquipmentsAsync(SearchEquipmentDto searchParameters);
    public Task<Equipment?> GetEquipmentByNameAsync(string name);
    public Task<Equipment?> GetEquipmentByIdAsync(int id);
    public Task DeleteEquipmentAsync(string? name);
    public Task<List<Equipment>> GetEquipmentListByNameAsync(string name);
    public Task RentEquipment(RentEquipmentDto dto);
    public Task<IEnumerable<Equipment>> GetAvailableEquipmentAsync();
    //public Task<List<int>> GetAvailableEquipmentIds();
   // public Task<List<int>> GetGameEquipmentIds(int gameId);
    public Task<IEnumerable<Equipment>> GetEquipmentByGameIdAsync(int gameId);
    public Task DeleteAllEquipmentByGameIdAsync(int gameId);
}