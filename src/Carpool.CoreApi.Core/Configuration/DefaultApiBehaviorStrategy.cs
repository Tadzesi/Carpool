using Carpool.CoreApi.Api.Core.Models;
using Carpool.CoreApi.Core.Helper;
using CorrelationId.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Carpool.CoreApi.Core.Configuration.ApiBehavior;

public static class DefaultApiBehaviorStrategy
{
    public static readonly Func<ActionContext, IActionResult> InvalidModelStateResponseFactory = (context) =>
    {
        ICorrelationContextAccessor? contextAccessor = context.HttpContext.RequestServices.GetRequiredService<ICorrelationContextAccessor>();
        ErrorResponse400 error = ResponseHelper.GetValidationErrorResponse(context.ModelState, contextAccessor.CorrelationContext);
        return new BadRequestObjectResult(error);
    };
}
