namespace battleshipAPI.Domain.Service;

public class BaseShipValidator : IBaseShipValidator
{
    public async Task<bool> ValidateContiguousOfShipCoordinates(List<List<int>> shipCoordinates)
    {
        bool contiguousOnX_axis = true;
        bool contiguousOnY_axis = true;

        // Check for x-axis contiguity
        for (int i = 1; i < shipCoordinates.Count; i++)
        {
            if (shipCoordinates[i][0] != shipCoordinates[i - 1][0] ||
            Math.Abs(shipCoordinates[i][1] - shipCoordinates[i - 1][1]) != 1)
            {
                contiguousOnX_axis = false;
                break;
            }
        }

        // Check for y-axis contiguity
        for (int i = 1; i < shipCoordinates.Count; i++)
        {
            if (shipCoordinates[i][1] != shipCoordinates[i - 1][1] ||
            Math.Abs(shipCoordinates[i][0] - shipCoordinates[i - 1][0]) != 1)
            {
                contiguousOnY_axis = false;
                break;
            }
        }

        return await Task.FromResult(contiguousOnX_axis || contiguousOnY_axis);
    }

    public async Task<bool> ValidateShipLength(int shipLength, List<List<int>> shipCoordinates)
    {
        return await Task.FromResult(shipLength == shipCoordinates.Count);
    }

    public async Task<bool> IsShipOnTheGrid(int xLength, int yLength, List<List<int>> shipCoordinates)
    {
        foreach (var coordinate in shipCoordinates)
        {
            if (coordinate[0] > xLength || coordinate[1] > yLength)
                return await Task.FromResult(false);
        }
        return await Task.FromResult(true);
    }
}
