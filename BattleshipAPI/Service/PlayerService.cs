using battleshipAPI.Common;
using battleshipAPI.Data;

using BattleshipAPI.Application;
using BattleshipAPI.Common;
using BattleshipAPI.Service.Interfaces;

using Microsoft.Extensions.Options;

namespace BattleshipAPI.Service;

public class PlayerService : IPlayerService
{
    private readonly ICoordinateValidator _coordinateValidator;
    private readonly ClassOfShips _shipOptions;

    public PlayerService(IOptions<ClassOfShips> shipOptions, ICoordinateValidator coordinateValidator)
    {
        _shipOptions = shipOptions.Value;
        _coordinateValidator = coordinateValidator;
    }

    public async Task<BaseResponse> SetPlayerCoordinates(Coordinates coordinates)
    {
        if (coordinates.BattleshipCoordinates is null)
            return await Task.FromResult(new BaseResponse(0, "Please place your battleship"));

        if (coordinates.DistroyerCoordinates is null || coordinates.DistroyerCoordinates.Count != _shipOptions.Destroyer.RequiredShipCount)
            return await Task.FromResult(new BaseResponse(0, "Please place your distroyers"));

        var (isValid, message) = await _coordinateValidator.ValidateShipCoordinate(coordinates);
        if (!isValid)
            return await Task.FromResult(new BaseResponse(0, message));

        (isValid, message) = await _coordinateValidator.ValidateShipOverlap(coordinates);
        if (!isValid)
            return await Task.FromResult(new BaseResponse(0, message));

        // set coordinates in app data
        PlayerCoordinates.SetPlayerCoordinates(coordinates.BattleshipCoordinates, coordinates.DistroyerCoordinates);

        return await Task.FromResult(new BaseResponse());
    }
}
