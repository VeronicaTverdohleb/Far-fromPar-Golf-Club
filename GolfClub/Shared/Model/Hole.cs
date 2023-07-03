using System.Text.Json.Serialization;

namespace Shared.Model;

public class Hole
{
    public int Number { get; set; }
    public int Par { get; set; }

    public Hole(int number, int par)
    {
        Number = number;
        Par = par;
    }
    private Hole() {}
}