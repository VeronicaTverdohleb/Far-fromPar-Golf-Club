namespace Shared.Dtos.EquipmentDto;

public class SearchEquipmentDto
{
   

    public string? NameContains { get; }
    
    public SearchEquipmentDto(string? nameContains)
    {
        NameContains = nameContains;
    }
}