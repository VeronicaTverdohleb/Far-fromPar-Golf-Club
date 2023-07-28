using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace Application.Logic;
/// <summary>
/// This Class takes care of the business logic for UserLogic
/// </summary>
public class UserLogic : IUserLogic
{
    private readonly IUserDao _userDao;

    /// <summary>
    /// Single argument constructor
    /// </summary>
    /// <param name="_userDao">IUserDao</param>
    public UserLogic(IUserDao _userDao)
    {
        this._userDao = _userDao;
    }

    /// <summary>
    /// Passes on the request to create a user by username
    /// </summary>
    /// <param name="userName">string userName</param>
    /// <returns>Task<User></returns>
    /// <exception cref="Exception">Throws exception if user not found</exception>
    public async Task<User> GetByUsernameAsync(string userName)
    {
        User? retrieved = await _userDao.GetByUsernameAsync(userName);
        if (retrieved == null)
        {
            throw new Exception($"User with name {userName} not found");
        }

        return retrieved;
    }

    /// <summary>
    /// Gets all users
    /// </summary>
    /// <returns>IEnumerable of all users</returns>
    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return _userDao.GetAllUsersAsync();
    }

    /// <summary>
    /// Passes on the request to create a new User to the UserDao
    /// </summary>
    /// <param name="user">UserCreationDto</param>
    /// <returns>Task<User></returns>
    /// <exception cref="Exception">throw exception if user already exists</exception>
    public async Task<User> CreateAsync(UserCreationDto user)
    {
        User? returned = await _userDao.GetByUsernameAsync(user.Username);
        if (returned != null)
        {
            throw new Exception("User already exists!");
        }
        if (string.IsNullOrEmpty(user.Name))
        {
            throw new Exception("Enter a name for the user!");
        }
        if (string.IsNullOrEmpty(user.Password))
        {
            throw new Exception("Enter a password for the user!");
        }
        if (string.IsNullOrEmpty(user.Username))
        {
            throw new Exception("Enter a username for the user!");
        }
        
        User created = await _userDao.CreateAsync(user);

        return created;
    }

    /// <summary>
    /// Passes on the request to delete a lesson to the UserDao
    /// </summary>
    /// <param name="userName">string</param>
    /// <exception cref="Exception">throws exception if user does not exists</exception>
    public async Task DeleteAsync(string userName)
    {
        User? received = await _userDao.GetByUsernameAsync(userName);
        if (received == null)
        {
            throw new Exception($"User with the Username {userName} does not exist");
        }

        await _userDao.DeleteAsync(userName);
    }
}