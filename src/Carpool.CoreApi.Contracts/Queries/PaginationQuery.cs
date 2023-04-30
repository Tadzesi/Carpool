using System.ComponentModel.DataAnnotations;

namespace Carpool.CoreApi.ApplicationCore.Contracts.Queries;

public class PaginationQuery
{
    private int? _offset;
    private int? _limit;

    /// <summary>
    /// The number of items to skip before starting to collect the result set. 0 &#x3D; 
    /// skip nothing, 10 &#x3D; skip first 10 results, 20 &#x3D; skip first 20 results.
    /// </summary>
    public int Offset
    {
        get => _offset == null || _offset < 0 ? 0 : _offset.Value;
        set => _offset = value;
    }

    /// <summary>
    /// The number of items to return, which can be less that the total number of items.
    /// </summary>
    [Range(1, 1000)]
    public int Limit
    {
        get => _limit ?? 100;
        set => _limit = value;
    }
}
