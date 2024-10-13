namespace BattleshipAPI.Application;

public class Coordinates
{
    public List<List<int>>? BattleshipCoordinates { get; set; }

    public Dictionary<string, List<List<int>>>? DistroyerCoordinates { get; set; }
}
