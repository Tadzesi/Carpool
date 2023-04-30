namespace Carpool.CoreApi.Api.Core.Models;

/// <summary>
/// Error response with 5001 status code
/// </summary>
public class ErrorResponse501 : ErrorResponse
{
    public ErrorResponse501()
    {
        Status = 501;
    }
}
