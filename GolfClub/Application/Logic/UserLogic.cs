using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao _userDao;

    public UserLogic(IUserDao _userDao)
    {
        this._userDao = _userDao;
    }

    public async Task<User> GetByUsernameAsync(string userName)
    {
        User? retrieved = await _userDao.GetByUsernameAsync(userName);
        if (retrieved == null)
        {
            throw new Exception($"Ingredient with name {userName} not found");
        }

        return retrieved;
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return _userDao.GetAllUsersAsync();
    }

    public async Task<User> CreateAsync(UserCreationDto user)
    {
        User? returned = await _userDao.GetByUsernameAsync(user.Username);
        if (returned != null)
        {
            throw new Exception("User already exists!");
        }

        User todo = new User(user.Name, user.Username, user.Password, "Member");
        User created = await _userDao.CreateAsync(todo);

        return created;
    }

    public async Task DeleteAsync(User user)
    {
        User? received = await _userDao.GetByUsernameAsync(user.UserName);
        if (received == null)
        {
            throw new Exception($"User with the Username {user.UserName} does not exist");
        }

        await _userDao.DeleteAsync(user);
    }
}