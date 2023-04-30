namespace Carpool.CoreApi.Core.Queries.Interfaces;

/// <summary>
/// Search parameter 
/// </summary>
public interface ISearchQuery
{
    string? Search { get; set; }
}
