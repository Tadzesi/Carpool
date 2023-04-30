

namespace Carpool.CoreApi.Core.Operations;

public class PagedListOperationResponse<T> : ListOperationResponse<T>
{
    /// <summary>
    /// Current presented page
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Requested page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total pages count
    /// </summary>
    public int PagesCount { get; set; }

    /// <summary>
    /// Total items 
    /// </summary>
    public int TotalItems { get; set; }

    public override void CopyFrom<TResponse>(TResponse other)
    {
        base.CopyFrom(other);

        try
        {
            if (typeof(TResponse).IsAssignableFrom(typeof(PagedListOperationResponse<T>))
                && other is PagedListOperationResponse<T> pagedResponse)
            {
                CurrentPage = pagedResponse.CurrentPage;
                PageSize = pagedResponse.PageSize;
                PagesCount = pagedResponse.PagesCount;
                TotalItems = pagedResponse.TotalItems;

            }
        }
        catch
        {
            // ignore
        }
    }
}
