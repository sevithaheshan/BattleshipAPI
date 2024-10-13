namespace battleshipAPI.Data;

public class PlayerCoordinates
{
    private static List<List<int>> BattleshipCoordinates;

    private static Dictionary<string, List<List<int>>> DistroyerCoordinates;

    public static void SetPlayerCoordinates(List<List<int>> battleshipCoordinates, Dictionary<string, List<List<int>>> distroyerCoordinates)
    {
        BattleshipCoordinates = battleshipCoordinates;
        DistroyerCoordinates = distroyerCoordinates;
    }

    public static List<List<int>> GetPlayerBattleshipCoordinates()
    {
        return BattleshipCoordinates;
    }

    public static Dictionary<string, List<List<int>>> GetPlayerDistroyerCoordinates()
    {
        return DistroyerCoordinates;
    }
}
