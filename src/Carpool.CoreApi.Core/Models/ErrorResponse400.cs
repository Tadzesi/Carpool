namespace Carpool.CoreApi.Api.Core.Models;

/// <summary>
/// Error response with 400 status code
/// </summary>
public class ErrorResponse400 : ErrorResponse
{
    public ErrorResponse400()
    {
        Status = 400;
    }
}
