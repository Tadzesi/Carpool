namespace Carpool.CoreApi.Core.Models;

/// <summary>
/// This object contains metadata about the total number of items, offset and limit in the result. This is always included for paginated and/or limited collections, regardless of whether the limit and/or offset are specified.
/// </summary>
public class Pagination
{
    /// <summary>
    /// The total number of items in the result
    /// </summary>
    /// <value>The total number of items in the result</value>
    public long? Total { get; set; }

    /// <summary>
    /// Value of the page size. 0 &#x3D; skip nothing, 10 &#x3D; skip first 10 results, 20 &#x3D; skip first 20 results.
    /// </summary>
    /// <value>Value of the offset. 0 &#x3D; skip nothing, 10 &#x3D; skip first 10 results, 20 &#x3D; skip first 20 results.</value>
    public int? PageSize { get; set; } = 20;

    /// <summary>
    /// Total pages count
    /// </summary>
    public int? PagesCount { get; set; }

    /// <summary>
    /// Current page 
    /// </summary>
    public int? Page { get; set; }

}
