namespace Shared.Dtos.EquipmentDto;

/// <summary>
/// Data Transfer Object used in Equipment-related use cases
/// </summary>
public class RentEquipmentDto
{
    public int GameId { get; }
    public List<int>? EquipmentIds { get; }


    public RentEquipmentDto(int gameId, List<int>? equipmentIds)
    {
        GameId = gameId;
        EquipmentIds = equipmentIds;
    }
}