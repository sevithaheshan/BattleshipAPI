namespace battleshipAPI.Common;

public class BaseResponse
{
    public int IsSuccess { get; private set; } = 1;
    public string Message { get; set; } = string.Empty;
    public object? Data { get; set; }

    public BaseResponse()
    {
    }

    public BaseResponse(int isSuccess)
    {
        this.IsSuccess = isSuccess;
    }

    public BaseResponse(string message)
    {
        this.Message = message;
    }

    public BaseResponse(int isSuccess, string message)
    {
        this.IsSuccess = isSuccess;
        this.Message = message;
    }

    public BaseResponse(int isSuccess, string message, object data)
    {
        this.IsSuccess = isSuccess;
        this.Message = message;
        this.Data = data;
    }
}
