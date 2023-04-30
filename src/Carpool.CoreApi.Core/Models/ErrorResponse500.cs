namespace Carpool.CoreApi.Api.Core.Models;

/// <summary>
/// Error response with 500 status code
/// </summary>
public class ErrorResponse500 : ErrorResponse
{
    public ErrorResponse500()
    {
        Status = 500;
    }
}
