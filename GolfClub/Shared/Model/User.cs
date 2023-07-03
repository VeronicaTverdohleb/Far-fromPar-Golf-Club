using System.Text.Json.Serialization;

namespace Shared.Model;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    
    [JsonIgnore]
    public ICollection<Game>? Games { get; set; }

    public User(string name, string userName, string password, string role)
    {
        Name = name;
        UserName = userName;
        Password = password;
        Role = role;
    }
    
    private User() { }
}