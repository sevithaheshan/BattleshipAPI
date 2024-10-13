namespace BattleshipAPI.Common;

public class ClassOfShips
{
    public const string ClassOfShipsPath = "ClassOfShips";

    public BattleshipSettings Battleship { get; set; }
    public DestroyerSettings Destroyer { get; set; }
}

public class BattleshipSettings
{
    public int Size { get; set; }
    public int RequiredShipCount { get; set; }
}

public class DestroyerSettings
{
    public int Size { get; set; }
    public int RequiredShipCount { get; set; }
}