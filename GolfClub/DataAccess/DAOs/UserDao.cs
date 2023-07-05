using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Model;

namespace DataAccess.DAOs;

public class UserDao : IUserDao
{
    private readonly DataContext context;
    
    public UserDao(DataContext context)
    {
        this.context = context;
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        IEnumerable<User> list = context.Users.ToList();
        return Task.FromResult(list);
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.UserName.ToLower().Equals(username.ToLower())
        );
        return existing;
    }

    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> added = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return added.Entity;
    }
    
    public async Task UpdateAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        User? existing = await GetByUsernameAsync(user.UserName);
        if (existing == null)
        {
            throw new Exception($"User with username {user.UserName} not found");
        }

        context.Users.Remove(existing);
        await context.SaveChangesAsync();
    }
}