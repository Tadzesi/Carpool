using Carpool.CoreApi.Core.Extensions;
using Carpool.CoreApi.Core.Operations;
using GuardNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Diagnostics;
using System.Net;

namespace Carpool.CoreApi.Core.Helpers;

public static class OperationExecutionHelper
{
    public static T Execute<T>(Action<T> code, ILogger? logger = null)
        where T : IOperationResponse, new()
    {
        Guard.NotNull(code, nameof(code));

        Exception? thrownException = null;
        var stackTrace = new StackTrace(true);
        var response = new T();

        try
        {
            code(response);
        }
        catch (DbUpdateException e)
        {
            HandleCatch(e.InnerException ?? e, response, logger);
            response.StatusCode = HttpStatusCode.Conflict;
            thrownException = e.InnerException ?? e;
        }
        catch (DBConcurrencyException e)
        {
            HandleCatch(e.InnerException ?? e, response, logger);
            response.StatusCode = HttpStatusCode.UnprocessableEntity;
            thrownException = e.InnerException ?? e;
        }
        catch (Exception e)
        {
            HandleCatch(e, response, logger);
            response.StatusCode = HttpStatusCode.BadRequest;
            thrownException = e;
        }
        finally
        {
            HandleFinally(stackTrace, thrownException, response, logger);
        }

        return response;
    }

    public static async Task<T> ExecuteAsync<T>(Func<T, Task> asyncCode, ILogger? logger = null)
        where T : IOperationResponse, new()
    {
        Guard.NotNull(asyncCode, nameof(asyncCode));

        Exception? thrownException = null;
        var stackTrace = new StackTrace(true);
        var response = new T();

        try
        {
            await asyncCode(response);
        }
        catch (DbUpdateException e)
        {
            HandleCatch(e.InnerException ?? e, response, logger);
            response.StatusCode = HttpStatusCode.Conflict;
            thrownException = e.InnerException ?? e;
        }
        catch (DBConcurrencyException e)
        {
            HandleCatch(e.InnerException ?? e, response, logger);
            response.StatusCode = HttpStatusCode.UnprocessableEntity;
            thrownException = e.InnerException ?? e;
        }
        catch (Exception e)
        {
            HandleCatch(e, response, logger);
            response.StatusCode = HttpStatusCode.BadRequest;
            thrownException = e;
        }
        finally
        {
            HandleFinally(stackTrace, thrownException, response, logger);
        }

        return response;
    }

    private static void HandleCatch<T>(Exception? exception, T response, ILogger? logger = null)
        where T : IOperationResponse
    {
        logger?.LogError(exception, "Error occurred : {error}", exception?.Message);

        if (response is OperationResponse operationResponse)
        {
            operationResponse.IsException = true;
            if (exception != null)
            {
                operationResponse.AddError(exception.Message);
                operationResponse.AddException(exception);
            }
            else
            {
                operationResponse.AddError("An unspecified error occurred while executing your request.");
            }
        }
    }

    private static void HandleFinally(StackTrace stackTrace,
                                      Exception? thrownException,
                                      IOperationResponse response,
                                      ILogger? logger = null)
    {
        var errors = new List<string>();
        if (thrownException != null)
        {
            Exception e = thrownException;
            while (e.InnerException != null)
            {
                e = e.InnerException;
            }

            errors.Add(e.Message);
        }

        string repositoryMethod = stackTrace.GetMethodName();
        string loggerMessage = response.GetExecuteWrapperFinalMessageForLogger(repositoryMethod, errors);
#pragma warning disable CA2254
        logger?.LogInformation(loggerMessage);
#pragma warning restore CA2254
    }
}
