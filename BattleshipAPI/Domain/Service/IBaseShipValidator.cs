namespace battleshipAPI.Domain.Service;

public interface IBaseShipValidator
{
    Task<bool> ValidateShipLength(int shipLength, List<List<int>> shipCoordinates);
    Task<bool> ValidateContiguousOfShipCoordinates(List<List<int>> shipCoordinates);
    Task<bool> IsShipOnTheGrid(int xLength, int yLength, List<List<int>> shipCoordinates);
}
