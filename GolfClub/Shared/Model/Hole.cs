using System.Text.Json.Serialization;

namespace Shared.Model;

public class Hole
{
    public int Number { get; set; }
    public int Par { get; set; }

    private Hole() {}
}