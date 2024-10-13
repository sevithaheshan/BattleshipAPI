using battleshipAPI.Common;

namespace battleshipAPI.Service.Interfaces;

public interface IFireService
{
    Task<BaseResponse> FireOnBot(List<int> firingPointCoordinate);
    Task<BaseResponse> FireOnPlayer();
}
