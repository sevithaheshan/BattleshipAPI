namespace BattleshipAPI.Domain;

public class Battleship : BaseShipModel
{
    public Battleship(int shipSize, List<List<int>> shipCoordinates) : base(shipSize, shipCoordinates)
    {
    }
}
