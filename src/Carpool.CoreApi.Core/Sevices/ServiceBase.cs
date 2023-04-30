using Carpool.CoreApi.Core.Helpers;
using Carpool.CoreApi.Core.Operations;
using Microsoft.Extensions.Logging;

namespace Carpool.CoreApi.Services;

public abstract class ServiceBase<TService> where TService : ServiceBase<TService>
{
    protected readonly ILogger<TService> Logger;

    protected ServiceBase(ILogger<TService> logger)
    {
        Logger = logger;
    }

    protected virtual T Execute<T>(Action<T> code)
        where T : IOperationResponse, new()
    {
        return OperationExecutionHelper.Execute<T>(code, Logger);
    }

    protected virtual async Task<T> ExecuteAsync<T>(Func<T, Task> asyncCode)
        where T : IOperationResponse, new()
    {
        return await OperationExecutionHelper.ExecuteAsync<T>(asyncCode, Logger);
    }
}
