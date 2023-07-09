using Shared.Dtos;
using Shared.Model;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    public Task<User> GetByUsernameAsync(string userName);
    public Task<IEnumerable<User>> GetAllUsersAsync();
    public Task<User> CreateAsync(UserCreationDto user);
    public Task DeleteAsync(string userName);
}