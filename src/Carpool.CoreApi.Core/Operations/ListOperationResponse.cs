using GuardNet;

namespace Carpool.CoreApi.Core.Operations;

public class ListOperationResponse<T> : OperationResponse
{
    public List<T>? Items { get; set; } = new List<T>();

    public override void CopyFrom<TResponse>(TResponse other)
    {
        Guard.NotNull(other, nameof(other));

        base.CopyFrom(other);

        try
        {
            if (typeof(TResponse).IsAssignableFrom(typeof(ListOperationResponse<T>)))
            {
                var listResponse = other as ListOperationResponse<T>;
                if (listResponse is not null)
                {
                    Items = listResponse.Items;
                }
            }
        }
        catch
        {
            Items = new List<T>();
        }
    }

    public new static ListOperationResponse<T> Failure => new() { IsException = true };
    public new static ListOperationResponse<T> Success => new();
}
