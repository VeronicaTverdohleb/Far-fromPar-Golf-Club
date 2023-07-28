namespace Shared.Dtos.EquipmentDto;

/// <summary>
/// Data Transfer Object used in Equipment-related use cases
/// </summary>
public class SearchEquipmentDto
{
    public string? NameContains { get; }

    public SearchEquipmentDto(string? nameContains)
    {
        NameContains = nameContains;
    }
}