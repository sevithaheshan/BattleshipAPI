namespace BattleshipAPI.Domain;

public class Distroyer : BaseShipModel
{
    public Distroyer(int shipSize, List<List<int>> shipCoordinates) : base(shipSize, shipCoordinates)
    {
    }
}
