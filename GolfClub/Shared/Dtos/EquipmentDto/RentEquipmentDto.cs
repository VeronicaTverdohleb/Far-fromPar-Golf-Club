namespace Shared.Dtos.EquipmentDto;

public class RentEquipmentDto
{

   
    public int GameId { get; }
    public List<string> EquipmentNames { get; }
    public int Amount { get; }
    
    public RentEquipmentDto(int gameId, List<string> equipmentNames, int amount)
    {

        GameId = gameId;
        EquipmentNames = equipmentNames;
        Amount = amount;
    }
}