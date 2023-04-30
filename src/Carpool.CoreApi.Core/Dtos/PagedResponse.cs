using Carpool.CoreApi.Core.Dtos;
using Carpool.CoreApi.Core.Models;

namespace WebTD.Api.Contracts.Dtos;

public class PagedResponse<T> : SimpleCollection<T>
{
    /// <summary>
    /// Pagination information
    /// </summary>
    public Pagination? Pagination { get; set; }
}
