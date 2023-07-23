using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos;
using Shared.Model;

namespace DataAccess.DAOs;
/// <summary>
/// This class interacts with the Database for UserLogic
/// </summary>
public class UserDao : IUserDao
{
    private readonly DataContext context;
    
    public UserDao(DataContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Retrieves a IEnumerable of all users with the role of Member
    /// </summary>
    /// <returns>IEnumerable of User</returns>
    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        IEnumerable<User> list = context.Users.Where(u => u.Role == "Member").ToList();
        return Task.FromResult(list);
    }

    /// <summary>
    /// Retrieves a user by its username
    /// </summary>
    /// <param name="username">string</param>
    /// <returns>Task of User</returns>
    public async Task<User> GetByUsernameAsync(string username)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.UserName.ToLower().Equals(username.ToLower())
        );
        return existing;
    }

    /// <summary>
    /// Creates a new User
    /// </summary>
    /// <param name="dto">UserCreationDto</param>
    /// <returns>Task of User</returns>
    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User user = new User(dto.Name, dto.Username, dto.Password,"Member");
        EntityEntry<User> added = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return added.Entity;
    }
        
    /// <summary>
    /// Deletes a User by userName
    /// </summary>
    /// <param name="userName">string</param>
    /// <exception cref="Exception">throws exception if user not found</exception>
    public async Task DeleteAsync(string userName)
    {
        User? existing = await GetByUsernameAsync(userName);
        if (existing == null)
        {
            throw new Exception($"User with username {userName} not found");
        }

        context.Users.Remove(existing);
        await context.SaveChangesAsync();
    }
}