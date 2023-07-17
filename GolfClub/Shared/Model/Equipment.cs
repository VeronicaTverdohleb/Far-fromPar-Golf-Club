using System.Text.Json.Serialization;

namespace Shared.Model;

public class Equipment
{

    public int Id { get; set; }
    public string? Name { get; set; }
    
    [JsonIgnore]
    public ICollection<Game>? Games { get; set; }

    public Equipment(string? name)
    {
        Name = name;
    }
    
    

    private Equipment() {}

  
}