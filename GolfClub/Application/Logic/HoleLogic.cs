using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Model;

namespace Application.Logic;

/// <summary>
/// Logic for Async methods used in the Web API
/// </summary>
public class HoleLogic : IHoleLogic
{
    private readonly IHoleDao holeDao;

    /// <summary>
    /// Constructor for HoleLogic
    /// </summary>
    /// <param name="holeDao"></param>
    public HoleLogic(IHoleDao holeDao)
    {
        this.holeDao = holeDao;
    }

    /// <summary>
    /// Returns a list of holes
    /// </summary>
    /// <returns>holes</returns>
    public Task<IEnumerable<Hole>> GetAsync()
    {
        return holeDao.GetAllHolesAsync();
    }
}