using GuardNet;

namespace Carpool.CoreApi.Core.Operations;

public class ItemOperationResponse<T> : OperationResponse
{
    public T? Item { get; set; }

    public override void CopyFrom<TResponse>(TResponse other)
    {
        Guard.NotNull(other, nameof(other));

        base.CopyFrom(other);

        try
        {
            if (typeof(TResponse).IsAssignableFrom(typeof(ItemOperationResponse<T>)))
            {
                var itemResponse = other as ItemOperationResponse<T>;
                if (itemResponse is not null)
                {
                    Item = itemResponse.Item;
                }
            }
        }
        catch
        {
            Item = default;
        }
    }

    public new static ItemOperationResponse<T> Failure => new() { IsException = true };
    public new static ItemOperationResponse<T> Success => new();
}
