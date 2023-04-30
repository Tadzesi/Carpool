namespace Carpool.CoreApi.Api.Core.Models;

/// <summary>
/// Error response with 404 status code
/// </summary>
public class ErrorResponse404 : ErrorResponse
{
    public ErrorResponse404()
    {
        Status = 404;
    }
}
