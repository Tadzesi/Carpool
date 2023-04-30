using Carpool.CoreApi.Core.Dtos;
using Carpool.CoreApi.Core.Models;
using Carpool.CoreApi.Core.Operations;
using Carpool.CoreApi.Core.Queries.Interfaces;
using GuardNet;
using System.Text;
using WebTD.Api.Contracts.Dtos;

namespace Carpool.CoreApi.Core.Extensions
{
    public static class OperationResponseExtensions
    {
        public static PagedResponse<TDto> ToCollection<TDto, TEntity>(
            this PagedListOperationResponse<TEntity> response,
            IPaginationQuery query,
            Converter<TEntity, TDto> expression)
        {
            return new PagedResponse<TDto>
            {
                Pagination = new Pagination
                {
                    PageSize = query.PageSize,
                    Page = query.Page,
                    PagesCount = response.TotalItems / query.PageSize,
                    Total = response.TotalItems
                },
                Items = response.Items.ConvertAll(expression)
            };
        }

        public static SimpleCollection<TDto> ToSimpleCollection<TDto, TEntity>(
            this ListOperationResponse<TEntity> response,
            Converter<TEntity, TDto> expression)
        {
            return new SimpleCollection<TDto>
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
}
