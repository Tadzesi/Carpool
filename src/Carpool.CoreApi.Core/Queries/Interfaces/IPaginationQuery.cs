namespace Carpool.CoreApi.Core.Queries.Interfaces;
public interface IPaginationQuery
{
    int Page { get; set; }
    int PageSize { get; set; }
}

