namespace Carpool.CoreApi.Api.Core.Models;

/// <summary>
/// Error response with 422 status code
/// </summary>
public class ErrorResponse422 : ErrorResponse
{
    public ErrorResponse422()
    {
        Status = 422;
    }
}
