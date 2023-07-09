using Shared.Dtos;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    public Task<ICollection<User>> GetAllUsersAsync();
    public Task<User> GetByUsernameAsync(string username);
    public Task CreateAsync(UserCreationDto user);
    public Task DeleteAsync(string userName);
}