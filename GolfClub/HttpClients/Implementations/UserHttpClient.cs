using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace HttpClients.Implementations;
/// <summary>
/// Class Responsible for making REST requests
/// </summary>
public class UserHttpClient : IUserService
{
    private readonly HttpClient client;

    public UserHttpClient(HttpClient client)
    {
        this.client = client;
    }
    /// <summary>
    /// Method requests all users
    /// </summary>
    /// <returns>ICollection of users</returns>
    /// <exception cref="Exception">throws exception if not successful</exception>
    public async Task<ICollection<User>> GetAllUsersAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/User");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<User> users = JsonSerializer.Deserialize<ICollection<User>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }
    /// <summary>
    /// Method requests a user by username
    /// </summary>
    /// <param name="username">string</param>
    /// <returns>User</returns>
    /// <exception cref="Exception">throws exception if not successful</exception>
    public async Task<User> GetByUsernameAsync(string username)
    {
        HttpResponseMessage response = await client.GetAsync($"/User/{username}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        User user = JsonSerializer.Deserialize<User>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return user;
    }
    /// <summary>
    /// Request to create a new user
    /// </summary>
    /// <param name="user">UserCreationDto</param>
    /// <exception cref="Exception">throws exception if not successful</exception>
    public async Task CreateAsync(UserCreationDto user)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/User", user);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
    /// <summary>
    /// Request to delete a user by username
    /// </summary>
    /// <param name="userName">string</param>
    /// <exception cref="Exception">throws exception if not successful</exception>
    public async Task DeleteAsync(string userName)
    {
        HttpResponseMessage response = await client.DeleteAsync($"/User/{userName}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}