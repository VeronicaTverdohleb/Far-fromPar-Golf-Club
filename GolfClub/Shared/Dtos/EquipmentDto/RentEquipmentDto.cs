namespace Shared.Dtos.EquipmentDto;

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