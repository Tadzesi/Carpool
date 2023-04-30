
namespace WebTD.Api.Contracts.Dtos;

public class ItemResponse<T>
{
    /// <summary>
    /// Returned items
    /// </summary>
    public T? Item { get; set; }
}
