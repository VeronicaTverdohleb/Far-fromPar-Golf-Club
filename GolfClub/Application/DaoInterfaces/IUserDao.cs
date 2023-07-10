using Shared.Model;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    public Task<IEnumerable<User>> GetAllUsersAsync();
    public Task<User?> GetByUsernameAsync(string username);
    public Task<User> CreateAsync(User user);
    public Task DeleteAsync(string username);
}