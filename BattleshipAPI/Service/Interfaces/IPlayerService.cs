using battleshipAPI.Common;

using BattleshipAPI.Application;

namespace BattleshipAPI.Service.Interfaces;

public interface IPlayerService
{
    Task<BaseResponse> SetPlayerCoordinates(Coordinates coordinates);
}
