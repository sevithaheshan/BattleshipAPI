using battleshipAPI.Common;
using battleshipAPI.Data;
using battleshipAPI.Service.Interfaces;

using BattleshipAPI.Common;

using Microsoft.Extensions.Options;

namespace battleshipAPI.Service;

public class FireService : IFireService
{
    private readonly GridSettings _gridSettings;

    public FireService(IOptions<GridSettings> gridSettings)
    {
        _gridSettings = gridSettings.Value;
    }

    public async Task<BaseResponse> FireOnBot(List<int> firingPointCoordinate)
    {
        if (firingPointCoordinate is null || firingPointCoordinate.Count != 2)
            return await Task.FromResult(new BaseResponse(0, "Firing point is not valid"));

        var btlCoordinates = BotCoordinates.GetBotBattleshipCoordinates();
        var dstryCoordinates = BotCoordinates.GetBotDistroyerCoordinates();

        if (btlCoordinates is null || dstryCoordinates is null)
            return await Task.FromResult(new BaseResponse(0, "Please place your ships first"));

        // check player has hit the bot's battleship 
        var response = await IsHit(btlCoordinates, firingPointCoordinate);
        if (!string.IsNullOrEmpty(response.Message))
            return await Task.FromResult(response);

        foreach (var dShip in dstryCoordinates)
        {
            // check player has hit the bot's distroyer
            response = await IsHit(dShip.Value, firingPointCoordinate);
            if (!string.IsNullOrEmpty(response.Message))
                return await Task.FromResult(response);
        }

        return await Task.FromResult(new BaseResponse());
    }

    public async Task<BaseResponse> FireOnPlayer()
    {
        var btlCoordinates = PlayerCoordinates.GetPlayerBattleshipCoordinates();
        var dstryCoordinates = PlayerCoordinates.GetPlayerDistroyerCoordinates();

        if (btlCoordinates is null || dstryCoordinates is null)
            return await Task.FromResult(new BaseResponse(0, "Please place your ships first"));

        var xCoordinate = GetRandomNumber(0, _gridSettings.XLength);
        var yCoordinate = GetRandomNumber(0, _gridSettings.YLength);

        // check bot has hit the player's battleship 
        var response = await IsHit(btlCoordinates, [xCoordinate, yCoordinate]);
        if (!string.IsNullOrEmpty(response.Message))
        {
            response.Data = new List<int>() { xCoordinate, yCoordinate };
            return await Task.FromResult(response);
        }

        foreach (var dShip in dstryCoordinates)
        {
            // check player has hit the bot's distroyer
            response = await IsHit(dShip.Value, [xCoordinate, yCoordinate]);
            if (!string.IsNullOrEmpty(response.Message))
            {
                response.Data = new List<int>() { xCoordinate, yCoordinate };
                return await Task.FromResult(response);
            }
        }

        return await Task.FromResult(new BaseResponse() { Data = new List<int>() { xCoordinate, yCoordinate } });
    }

    #region Private Methods
    private async Task<BaseResponse> IsHit(List<List<int>> coordinates, List<int> firingPointCoordinate)
    {
        var isHit = coordinates.Any(coord => coord.SequenceEqual(firingPointCoordinate));

        if (isHit)
            return await Task.FromResult(new BaseResponse("Ship was hit"));

        return await Task.FromResult(new BaseResponse());
    }

    private static int GetRandomNumber(int minBoundry, int maxBoundry)
    {
        return new Random().Next(minBoundry, maxBoundry);
    }
    #endregion
}
