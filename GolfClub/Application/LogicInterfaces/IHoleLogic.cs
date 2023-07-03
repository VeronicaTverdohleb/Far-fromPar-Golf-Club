using Shared.Model;

namespace Application.LogicInterfaces;

public interface IHoleLogic
{
    Task<IEnumerable<Hole>> GetAsync();
}