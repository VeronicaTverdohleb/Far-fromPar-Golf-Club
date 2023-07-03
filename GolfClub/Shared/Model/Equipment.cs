using System.Text.Json.Serialization;

namespace Shared.Model;

public class Equipment
{
    public string Name { get; set; }
    public int Amount { get; set; }
    
    [JsonIgnore]
    public ICollection<Game>? Games { get; set; }

    private Equipment() {}
}