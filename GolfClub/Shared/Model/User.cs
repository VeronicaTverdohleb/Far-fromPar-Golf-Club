using System.Text.Json.Serialization;

namespace Shared.Model;

public class User
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    
    [JsonIgnore]
    public ICollection<Game>? Games { get; set; }

    private User() { }
    
    public User(string name, string userName, string password, string role)
    {
        Name = name;
        UserName = userName;
        Password = password;
        Role = role;
    }
    
    public User(string Name, string UserName, string Password)
    {
        this.Name = Name;
        this.UserName = UserName;
        this.Password = Password;
        Role = "Member";
    }
}