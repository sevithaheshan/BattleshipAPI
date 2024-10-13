using battleshipAPI.Domain.Service;

using BattleshipAPI.Application;
using BattleshipAPI.Common;
using BattleshipAPI.Domain;
using BattleshipAPI.Service.Interfaces;

using Microsoft.Extensions.Options;

namespace BattleshipAPI.Service;

public class CoordinateValidator : ICoordinateValidator
{
    private readonly ClassOfShips _shipOptions;
    private readonly GridSettings _gridSettings;

    private readonly IBaseShipValidator _baseShipValidator;

    public CoordinateValidator(IOptions<ClassOfShips> shipOptions, IOptions<GridSettings> gridSettings, IBaseShipValidator baseShipValidator)
    {
        _shipOptions = shipOptions.Value;
        _gridSettings = gridSettings.Value;
        _baseShipValidator = baseShipValidator;
    }

    public async Task<(bool isValid, string message)> ValidateShipCoordinate(Coordinates coordinates)
    {
        var battleShip = new Battleship(_shipOptions.Battleship.Size, coordinates.BattleshipCoordinates);

        var (isValid, message) = await Validate(battleShip.ShipSize, battleShip.ShipCoordinates);

        if (!isValid)
            return await GenerateResponse(isValid, message);

        foreach (var destroyer in coordinates.DistroyerCoordinates)
        {
            var dstryer = new Distroyer(_shipOptions.Destroyer.Size, destroyer.Value);

            (isValid, message) = await Validate(dstryer.ShipSize, dstryer.ShipCoordinates);

            if (!isValid)
                return await GenerateResponse(isValid, message);
        }

        return await Task.FromResult((true, ""));
    }

    public async Task<(bool isValid, string message)> ValidateShipOverlap(Coordinates coordinates)
    {
        var allCoordinates = new List<List<int>>();
        var seenCoordinates = new HashSet<(int, int)>();

        foreach (var battleshipCoordinate in coordinates.BattleshipCoordinates)
        {
            allCoordinates.Add(battleshipCoordinate);
        }

        foreach (var destroyer in coordinates.DistroyerCoordinates)
        {
            foreach (var distroyerCoordinate in destroyer.Value)
            {
                allCoordinates.Add(distroyerCoordinate);
            }
        }

        foreach (var coordinate in allCoordinates)
        {
            var coordinateTuple = (coordinate[0], coordinate[1]);

            if (!seenCoordinates.Add(coordinateTuple))
            {
                return await Task.FromResult((false, "Ships can not be overlapped"));
            }
        }

        return await Task.FromResult((true, ""));
    }

    #region PrivateMethods
    private async Task<(bool isValid, string message)> Validate(int shipSize, List<List<int>> shipCoordinates)
    {
        bool isLengthValid = await _baseShipValidator.ValidateShipLength(shipSize, shipCoordinates);
        if (!isLengthValid)
            return await GenerateResponse(isLengthValid, "ship size is not the required size");

        bool isContiguous = await _baseShipValidator.ValidateContiguousOfShipCoordinates(shipCoordinates);
        if (!isContiguous)
            return await GenerateResponse(isContiguous, "ship is not contiguous on X-axis or Y-axis");

        bool isShipOnGrid = await _baseShipValidator.IsShipOnTheGrid(_gridSettings.XLength, _gridSettings.YLength, shipCoordinates);
        if (!isShipOnGrid)
            return await GenerateResponse(isShipOnGrid, "ship is not in the defined grid");

        return await Task.FromResult((true, ""));
    }

    private async Task<(bool isValid, string message)> GenerateResponse(bool isValid, string message)
    {
        return await Task.FromResult((isValid, message));
    }
    #endregion
}
