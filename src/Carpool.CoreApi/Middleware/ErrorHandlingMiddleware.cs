using Carpool.CoreApi.Api.Core.Models;
using CorrelationId;
using CorrelationId.Abstractions;

namespace Carpool.CoreApi.Middleware;

/// <summary>
/// Api Exception Handling Middleware
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly ICorrelationContextAccessor _correlationAccessor;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        ICorrelationContextAccessor correlationAccessor,
        IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _correlationAccessor = correlationAccessor;
        _environment = environment;
    }

    public async Task Invoke(HttpContext context)
    {
        CorrelationContext correlationContext = _correlationAccessor.CorrelationContext;
        string correlationId = correlationContext.CorrelationId;

        _logger.LogDebug("Starting error handling middleware with Correlation ID: {CId}", correlationId);

        try
        {
            await _next(context).ConfigureAwait(false);
        }

        catch (Exception ex)
        {
            LogException(ex, correlationId);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var error = new ErrorResponse
            {
                CorrelationId = correlationId,
                Message = "Something unexpected happenned during the request execution",
                ErrorCode = "N/A"
            };

            FillDeveloperProperties(error, ex);
            await context.Response.WriteAsJsonAsync(error);
        }

        _logger.LogDebug("Error handling middleware with Correlation ID: {CId} has finished", correlationId);

    }

    private void LogException(Exception e, string correlationId)
    {
        _logger.LogError(
            e,
            "Error occurred while executing request: {Err} Correlation ID: {CId}",
            e.Message, correlationId);

        Exception? inner = e.InnerException;
        while (inner != null)
        {
            _logger.LogError(inner, "Inner exception details: {Err}", inner.Message);
            inner = inner.InnerException;
        }
    }

    /// <summary>
    /// Fill development exception in case is development
    /// </summary>
    /// <param name="error"></param>
    /// <param name="e"></param>
    private void FillDeveloperProperties(ErrorResponse error, Exception e)
    {
        if (_environment.IsDevelopment())
        {
            error.Message += $": {e.Message}";
            error.DeveloperMessage = e.StackTrace;
        }
    }
}

