using System.Net;

namespace Carpool.CoreApi.Core.Operations;

public interface IOperationResponse
{
    bool Succeeded { get; }
    bool IsException { get; }
    HttpStatusCode? StatusCode { get; set; }
    Exception? ThrownException { get; set; }
    string? StackTrace { get; }
    List<string> Errors { get; }
    List<string> Warnings { get; }
    List<string> Infos { get; }
}
