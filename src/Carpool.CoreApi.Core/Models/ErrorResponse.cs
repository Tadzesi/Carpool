using System.ComponentModel.DataAnnotations;

namespace Carpool.CoreApi.Api.Core.Models;

/// <summary>
/// Generic format for an error message
/// </summary>
public class ErrorResponse : IErrorResponse
{
    /// <summary>
    /// The HTTP Status code that was sent with this error message
    /// </summary>
    /// <value>The HTTP Status code that was sent with this error message</value>
    [Range(100, 600)]
    public int Status { get; set; }

    /// <summary>
    /// Internal error code
    /// </summary>
    /// <value>Internal error code</value>
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Optional basic feedback message in your language
    /// </summary>
    /// <value>Optional basic feedback message in your language</value>
    public string? Message { get; set; }

    /// <summary>
    /// Optional hyperlink to documentation about this error
    /// </summary>
    /// <value>Optional hyperlink to documentation about this error</value>
    public string? Link { get; set; }

    /// <summary>
    /// Optional more technical error message
    /// </summary>
    /// <value>Optional more technical error message</value>
    public string? DeveloperMessage { get; set; }

    /// <summary>
    /// An optional list of validation messages
    /// </summary>
    /// <value>An optional list of validation messages</value>
    public List<ValidationMessage>? ValidationMessages { get; set; }

    /// <summary>
    /// A UUID that contains a correlationId which can be used to correlate multiple errors and link this error to entries in (other) logs.
    /// </summary>
    /// <value>A UUID that contains a correlationId which can be used to correlate multiple errors and link this error to entries in (other) logs.</value>
    public string? CorrelationId { get; set; }
}
