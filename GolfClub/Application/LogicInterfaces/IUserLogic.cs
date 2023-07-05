using Shared.Dtos;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    public Task<IEnumerable<User>> GetAllUsersAsync();
    public Task<User> CreateAsync(UserCreationDto user);
    public Task DeleteAsync(User user);
}