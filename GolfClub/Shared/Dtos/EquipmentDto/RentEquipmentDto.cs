namespace Shared.Dtos.EquipmentDto;

public class RentEquipmentDto
{
   

    public int GameId { get; }
    public List<int>? EquipmentIds { get; }
    public int Amount { get; }
    
  /*  public RentEquipmentDto(int gameId, List<string> equipmentNames)
    {

        GameId = gameId;
        EquipmentNames = equipmentNames;
        
    }*/
    public RentEquipmentDto(int gameId, List<int>? equipmentIds)
    {
        GameId = gameId;
        EquipmentIds = equipmentIds;
    }
}