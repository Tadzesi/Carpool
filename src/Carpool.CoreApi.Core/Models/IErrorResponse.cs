namespace Carpool.CoreApi.Api.Core.Models;

public interface IErrorResponse
{
    public int Status { get; set; }
    public string? ErrorCode { get; set; }
    public string? Message { get; set; }
    public string? DeveloperMessage { get; set; }
    public List<ValidationMessage>? ValidationMessages { get; set; }
    public string? CorrelationId { get; set; }
}
