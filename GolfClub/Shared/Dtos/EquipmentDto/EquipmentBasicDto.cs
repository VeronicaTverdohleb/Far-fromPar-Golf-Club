using Shared.Model;

namespace Shared.Dtos.EquipmentDto;

public class EquipmentBasicDto
{

    public int Id { get; set; }
    public string? Name { get; set; }
    public int Amount { get; set; }




    public EquipmentBasicDto(string? name, int amount)
    {

        Name = name;
        Amount = amount;
    }

    public EquipmentBasicDto(string? name)
    {
        Name = name;
    }

    public EquipmentBasicDto()
    {
    }
}

  