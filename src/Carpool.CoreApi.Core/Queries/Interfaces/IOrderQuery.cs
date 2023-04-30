namespace Carpool.CoreApi.Core.Queries.Interfaces;

public interface IOrderQuery
{
    string OrderBy { get; set; }

    bool OrderDesc { get; set; }
}
