using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IHoleService
{
    Task<ICollection<Hole>> GetAllHolesAsync();
}