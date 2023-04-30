namespace Carpool.CoreApi.Contracts.Contracts.Dtos;

public class SimpleCollectionDto<T>
{
    /// <summary>
    /// Returned items
    /// </summary>
    public List<T>? Items { get; set; }
}
