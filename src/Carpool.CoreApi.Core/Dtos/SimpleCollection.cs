namespace Carpool.CoreApi.Core.Dtos;

public class SimpleCollection<T>
{
    /// <summary>
    /// Returned items
    /// </summary>
    public List<T>? Items { get; set; }
}
