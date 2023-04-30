namespace Carpool.CoreApi.Api.Core.Models;

/// <summary>
/// Error response with 409 status code
/// </summary>
public class ErrorResponse409 : ErrorResponse
{
    public ErrorResponse409()
    {
        Status = 409;
    }
}
