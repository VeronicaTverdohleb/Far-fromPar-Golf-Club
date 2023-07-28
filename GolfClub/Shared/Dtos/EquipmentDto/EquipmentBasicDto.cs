using Shared.Model;

namespace Shared.Dtos.EquipmentDto;

/// <summary>
/// Data Transfer Object used in Equipment-related use cases
/// </summary>
public class EquipmentBasicDto
{
    public string? Name { get; set; }
    public int Amount { get; set; }


    public EquipmentBasicDto(string? name)
    {
        Name = name;
    }

    public EquipmentBasicDto()
    {
    }
}