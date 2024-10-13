using BattleshipAPI.Application;

namespace BattleshipAPI.Service.Interfaces;

public interface ICoordinateValidator
{
    Task<(bool isValid, string message)> ValidateShipCoordinate(Coordinates coordinates);
    Task<(bool isValid, string message)> ValidateShipOverlap(Coordinates coordinates);
}
