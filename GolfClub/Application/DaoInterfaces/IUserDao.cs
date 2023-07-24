using Shared.Dtos;
using Shared.Model;

namespace Application.DaoInterfaces;
/// <summary>
/// This class is the interface for the UserDao
/// </summary>
public interface IUserDao
{
    public Task<IEnumerable<User>> GetAllUsersAsync();
    public Task<User?> GetByUsernameAsync(string username);
    public Task<User> CreateAsync(UserCreationDto user);
    public Task DeleteAsync(string username);
}