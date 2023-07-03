using Shared.Model;

namespace Application.DaoInterfaces;

public interface IHoleDao
{
    public Task<IEnumerable<Hole>> GetAllHolesAsync();
}