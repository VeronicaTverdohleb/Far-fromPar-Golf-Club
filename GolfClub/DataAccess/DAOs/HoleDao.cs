using Application.DaoInterfaces;
using Shared.Model;

namespace DataAccess.DAOs;

/// <summary>
/// Data Access Object responsible for interacting with the database
/// Used in HoleLogic
/// </summary>
public class HoleDao : IHoleDao
{
    private readonly DataContext context;

    public HoleDao(DataContext context)
    {
        this.context = context;
    }
    public Task<IEnumerable<Hole>> GetAllHolesAsync()
    {
        IEnumerable<Hole> result = context.Holes.AsEnumerable();

        return Task.FromResult(result);
    }
}