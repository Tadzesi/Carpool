using Carpool.CoreApi.Api.Core.Models;
using Carpool.CoreApi.Core.Extensions;
using Carpool.CoreApi.Core.Operations;
using CorrelationId;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace Carpool.CoreApi.Core.Helper;

public static class ResponseHelper
{
    public static IErrorResponse GetErrorResponse(
        OperationResponse response,
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment environment,
        CorrelationContext correlation,
        string? errorCode = null,
        string? link = null)
    {
        string message = response.GetFormattedErrorMessage("; "); // easier for api tests to parse errors
        string? correlationId = correlation?.CorrelationId;

        IErrorResponse error = response.StatusCode switch
        {
            HttpStatusCode.BadRequest => new ErrorResponse400
            {
                CorrelationId = correlationId,
                Message = message,
                ErrorCode = errorCode ?? nameof(HttpStatusCode.BadRequest),
                Link = link
            },
            HttpStatusCode.NotFound => new ErrorResponse404
            {
                CorrelationId = correlationId,
                Message = message,
                ErrorCode = errorCode ?? nameof(HttpStatusCode.NotFound),
                Link = link
            },
            HttpStatusCode.Conflict => new ErrorResponse409
            {
                CorrelationId = correlationId,
                Message = message,
                ErrorCode = errorCode ?? nameof(HttpStatusCode.Conflict),
                Link = link
            },
            HttpStatusCode.UnprocessableEntity => new ErrorResponse422
            {
                CorrelationId = correlationId,
                Message = message,
                ErrorCode = errorCode ?? nameof(HttpStatusCode.UnprocessableEntity),
                Link = link
            },
            HttpStatusCode.NotImplemented => new ErrorResponse501
            {
                CorrelationId = correlationId,
                Message = message,
                ErrorCode = errorCode ?? nameof(HttpStatusCode.NotImplemented),
                Link = link
            },
            _ => new ErrorResponse400
            {
                CorrelationId = correlationId,
                Message = message,
                ErrorCode = errorCode ?? nameof(HttpStatusCode.BadRequest),
                Link = link
            },
        };

        if (environment.IsDevelopment())
        {
            error.DeveloperMessage = response.StackTrace;
        }

        return error;
    }

    public static ErrorResponse400 GetValidationErrorResponse(
        ModelStateDictionary modelState,
        CorrelationContext correlationContext)
    {
        var validationMessages = new List<ValidationMessage>();
        foreach (KeyValuePair<string, ModelStateEntry> item in modelState)
        {
            string? key = item.Key;
            foreach (ModelError? i in item.Value.Errors)
            {
                validationMessages.Add(new ValidationMessage
                {
                    Field = key, //.ToSnakeCase(),
                    Message = i.ErrorMessage
                });
            }
        }

        return new ErrorResponse400
        {
            ErrorCode = nameof(HttpStatusCode.BadRequest),
            CorrelationId = correlationContext.CorrelationId,
            Message = "One or more validation errors occured",
            ValidationMessages = validationMessages
        };
    }
}
