namespace Shared.Dtos;
/// <summary>
/// Data Transfer Object used in User-related use cases
/// </summary>
public class UserCreationDto
{

    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public UserCreationDto(string name, string username, string password)
    {
        Name = name;
        Username = username;
        Password = password;
    }
}