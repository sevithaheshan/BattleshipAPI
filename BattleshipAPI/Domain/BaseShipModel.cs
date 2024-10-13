namespace BattleshipAPI.Domain;

public class BaseShipModel
{
    public List<List<int>> ShipCoordinates { get; init; }
    public int ShipSize { get; init; }

    public BaseShipModel(int shipSize, List<List<int>> shipCoordinatesOnGrid)
    {
        ShipSize = shipSize;
        ShipCoordinates = shipCoordinatesOnGrid;
    }
}
