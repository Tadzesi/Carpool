using System.Text;

namespace Carpool.CoreApi.Contracts.Contracts.Dtos;

/// <summary>
/// This object contains metadata about the total number of items, offset and limit in the result. This is always included for paginated and/or limited collections, regardless of whether the limit and/or offset are specified.
/// </summary>
public class PaginationDto
{
    /// <summary>
    /// The total number of items in the result
    /// </summary>
    /// <value>The total number of items in the result</value>
    public long? Total { get; set; }

    /// <summary>
    /// Value of the offset. 0 &#x3D; skip nothing, 10 &#x3D; skip first 10 results, 20 &#x3D; skip first 20 results.
    /// </summary>
    /// <value>Value of the offset. 0 &#x3D; skip nothing, 10 &#x3D; skip first 10 results, 20 &#x3D; skip first 20 results.</value>
    public int? Offset { get; set; }

    /// <summary>
    /// Value of limit, the max number of items returned in this result
    /// </summary>
    /// <value>Value of limit, the max number of items returned in this result</value>
    public int? Limit { get; set; }

    /// <summary>
    /// Returns the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class Pagination {\n");
        sb.Append("  Total: ").Append(Total).Append('\n');
        sb.Append("  Offset: ").Append(Offset).Append('\n');
        sb.Append("  Limit: ").Append(Limit).Append('\n');
        sb.Append("}\n");
        return sb.ToString();
    }
}
