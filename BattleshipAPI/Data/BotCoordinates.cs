namespace battleshipAPI.Data;

public class BotCoordinates
{
    private static List<List<int>> BattleshipCoordinates;

    private static Dictionary<string, List<List<int>>> DistroyerCoordinates;

    public static void SetBotCoordinates(List<List<int>> battleshipCoordinates, Dictionary<string, List<List<int>>> distroyerCoordinates)
    {
        BattleshipCoordinates = battleshipCoordinates;
        DistroyerCoordinates = distroyerCoordinates;
    }

    public static List<List<int>> GetBotBattleshipCoordinates()
    {
        return BattleshipCoordinates;
    }

    public static Dictionary<string, List<List<int>>> GetBotDistroyerCoordinates()
    {
        return DistroyerCoordinates;
    }
}
