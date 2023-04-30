using Carpool.CoreApi.ApplicationCore.Contracts.Queries;
using Carpool.CoreApi.Contracts.Contracts.Dtos;
using Carpool.CoreApi.Core.Operations;
using GuardNet;
using System.Text;

namespace Carpool.CoreApi.ApplicationCore.Extensions;

public static class OperationResponseExtensions
{
    public static CollectionDto<TDto> ToCollection<TDto, TEntity>(
        this ListOperationResponse<TEntity> response,
        PaginationQuery query,
        Converter<TEntity, TDto> expression)
    {
        return new CollectionDto<TDto>
        {
            Pagination = new PaginationDto
            {
                Offset = query.Offset,
                Limit = query.Limit,
                Total = response.Items!.Count
            },
            Items = response.Items!.ConvertAll(expression)
        };
    }

    public static SimpleCollectionDto<TDto> ToSimpleCollection<TDto, TEntity>(
        this ListOperationResponse<TEntity> response,
        Converter<TEntity, TDto> expression)
    {
        return new SimpleCollectionDto<TDto>
        {
            Items = response.Items!.ConvertAll(expression)
        };
    }

    public static string GetFormattedErrorMessage(this IOperationResponse response)
    {
        Guard.NotNull(response, nameof(response));
        return response.GetFormattedErrorMessage(" --|-- ");
    }

    public static string GetFormattedErrorMessage(this IOperationResponse response, string separator)
    {
        Guard.NotNull(response, nameof(response));
        Guard.NotNullOrWhitespace(separator, nameof(separator));
        return response.Errors != null && response.Errors.Count > 0
            ? string.Join(separator, response.Errors)
            : string.Empty;
    }

    public static string GetExecuteWrapperFinalMessageForLogger(this IOperationResponse response, string serviceMethodName, List<string> errors)
    {
        Guard.NotNull(response, nameof(response));
        Guard.NotNullOrEmpty(serviceMethodName, nameof(serviceMethodName));

        var builder = new StringBuilder();
        builder.Append("Method: ").AppendLine(serviceMethodName);
        builder.Append("--> Success: ").AppendLine(response.Succeeded.ToString());
        if (errors?.Count > 0)
        {
            builder.AppendLine("--> Errors: ");

            foreach (string error in errors)
            {
                builder.Append("----> ").AppendLine(error);
            }
        }

        return builder.ToString();
    }
}
