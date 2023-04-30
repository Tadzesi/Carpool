namespace Carpool.CoreApi.Contracts.Contracts.Dtos;

public class CollectionDto<T> : SimpleCollectionDto<T>
{
    /// <summary>
    /// Pagination information
    /// </summary>
    public PaginationDto? Pagination { get; set; }
}
