using battleshipAPI.Data;
using battleshipAPI.Service.Interfaces;

using BattleshipAPI.Common;

using Microsoft.Extensions.Options;

namespace battleshipAPI.Service;

public class BotService : IBotService
{
    private readonly ClassOfShips _shipOptions;
    private readonly GridSettings _gridSettings;

    private List<List<int>> BattleshipCoordinates = [];
    private Dictionary<string, List<List<int>>> DistroyerCoordinates = [];

    private List<int> GeneratedYCoordinate = [];

    public BotService(IOptions<ClassOfShips> shipOptions, IOptions<GridSettings> gridSettings)
    {
        _shipOptions = shipOptions.Value;
        _gridSettings = gridSettings.Value;
    }

    public async Task SetBotCoordinates()
    {
        // ship always is placed on X axis.
        // Battleship coordinates
        var bc = await GetShipCoordinates(_shipOptions.Battleship.Size);
        BattleshipCoordinates = bc;

        // ship always is placed on X axis.
        // Distroyer coordinates
        for (int i = 1; i <= _shipOptions.Destroyer.RequiredShipCount; i++)
        {
            var dc = await GetShipCoordinates(_shipOptions.Destroyer.Size);
            DistroyerCoordinates.Add($"distroyer_{i}", dc);
        }

        BotCoordinates.SetBotCoordinates(BattleshipCoordinates, DistroyerCoordinates);
    }

    #region Private Methods
    private async Task<List<List<int>>> GetShipCoordinates(int shipLength)
    {
        List<List<int>> BattleshipCoordinates = [];

        var yCoordinate = 0;
        var xCoordinate = GetRandomNumber(0, _gridSettings.XLength);

        // this prevents the overlapping of ships
        if (GeneratedYCoordinate.Any())
        {
            do
            {
                yCoordinate = GetRandomNumber(0, _gridSettings.YLength);
            } while (GeneratedYCoordinate.Contains(yCoordinate));
        }
        else
        {
            yCoordinate = GetRandomNumber(0, _gridSettings.YLength);
            GeneratedYCoordinate.Add(yCoordinate);
        }

        var isMoveableOnX = CanMoveForwardOnAxis(xCoordinate, shipLength, _gridSettings.XLength);

        if (isMoveableOnX)
        {
            for (int i = 0; i < shipLength; i++)
            {
                var coordinate = new List<int>
                {
                    xCoordinate,
                    yCoordinate
                };
                BattleshipCoordinates.Add(coordinate);
                xCoordinate++;
            }
        }
        else
        {
            for (int i = 0; i < shipLength; i++)
            {
                var coordinate = new List<int>
                {
                    xCoordinate,
                    yCoordinate
                };
                BattleshipCoordinates.Add(coordinate);
                xCoordinate--;
            }
        }

        return await Task.FromResult(BattleshipCoordinates);
    }

    private static bool CanMoveForwardOnAxis(int point, int shipLength, int axisLength)
    {
        return (point + shipLength <= axisLength);
    }

    private static int GetRandomNumber(int minBoundry, int maxBoundry)
    {
        return new Random().Next(minBoundry, maxBoundry);
    }
    #endregion
}
