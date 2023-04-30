using GuardNet;
using System.Net;

namespace Carpool.CoreApi.Core.Operations;

public class OperationResponse : IOperationResponse
{
    private bool _overrideSucceeded;

    public OperationResponse()
    {
        Errors = new List<string>();
        Warnings = new List<string>();
        Infos = new List<string>();
    }

    public bool Succeeded => (Errors?.Count == 0 && !IsException) || _overrideSucceeded;
    public bool ContainsErrors => Errors?.Count > 0 || IsException;
    public bool CompleteTransactionIfError { get; private set; }
    public bool IsException { get; set; }
    public HttpStatusCode? StatusCode { get; set; }
    public Exception? ThrownException { get; set; }
    public string? StackTrace { get; set; }
    public List<string> Errors { get; set; }
    public List<string> Warnings { get; set; }
    public List<string> Infos { get; set; }

    public OperationResponse AddError(string error)
    {
        Errors?.Add(error);
        return this;
    }

    public OperationResponse AddWarning(string warning)
    {
        Warnings?.Add(warning);
        return this;
    }

    public OperationResponse AddInfo(string info)
    {
        Infos?.Add(info);
        return this;
    }

    public OperationResponse AddException(Exception exception)
    {
        ThrownException = exception;
        if (exception.StackTrace != null)
        {
            AddStackTrace(exception.StackTrace);
        }

        return this;
    }

    public OperationResponse AddStackTrace(string stackTrace)
    {
        StackTrace = stackTrace;
        return this;
    }

    /// <summary>
    /// If your response is unsuccessful, the `DbTransactionMiddleware` will not commit
    /// the transaction created in the beginning of the request. To override such functionality,
    /// you can override property called `CompleteTransactionIfError` to true, which will then
    /// translate to transaction being committed, even in case there are errors.
    /// </summary>
    /// <returns></returns>
    public OperationResponse MarkAsCompleteTransactionIfError()
    {
        if (Succeeded == false)
        {
            CompleteTransactionIfError = true;
        }

        return this;
    }

    public OperationResponse MarkAsSuccessful()
    {
        _overrideSucceeded = true;
        return this;
    }

    public OperationResponse MarkAsSuccessfulIfNoErrors()
    {
        // set override flag only if there is no error and IsException is false
        if (Errors?.Count == 0 && !IsException)
        {
            _overrideSucceeded = true;
        }

        return this;
    }

    public virtual void CopyFrom<TResponse>(TResponse other) where TResponse : OperationResponse
    {
        Guard.NotNull(other, nameof(other));

        if (other.Errors != null && other.Errors.Count > 0)
        {
            Errors?.AddRange(other.Errors);
        }

        if (other.Warnings != null && other.Warnings.Count > 0)
        {
            Warnings?.AddRange(other.Warnings);
        }

        if (other.Infos != null && other.Infos.Count > 0)
        {
            Infos?.AddRange(other.Infos);
        }

        IsException = other.IsException;
        StackTrace = other.StackTrace;
        StatusCode = other.StatusCode;
    }

    public static OperationResponse Success => new();
    public static OperationResponse Failure => new() { IsException = true };

    public static OperationResponse FromException(Exception exception, HttpStatusCode? statusCode = null)
    {
        Guard.NotNull(exception, nameof(exception));

        OperationResponse failure = Failure;
        failure.AddError(exception.Message);
        failure.AddStackTrace(exception.StackTrace!);
        failure.IsException = true;
        failure.StatusCode = statusCode ?? HttpStatusCode.BadRequest;

        return failure;
    }

    public static OperationResponse FailureWithError(string error, HttpStatusCode? statusCode = null)
    {
        Guard.NotNullOrWhitespace(error, nameof(error));

        OperationResponse failure = Failure;
        failure.AddError(error);
        failure.StatusCode = statusCode ?? HttpStatusCode.BadRequest;
        return failure;
    }

    public static OperationResponse FailureWithErrors(params string[] errors)
    {
        Guard.NotNull(errors, nameof(errors));

        OperationResponse failure = Failure;
        foreach (string error in errors)
        {
            failure.AddError(error);
        }

        return failure;
    }
}
