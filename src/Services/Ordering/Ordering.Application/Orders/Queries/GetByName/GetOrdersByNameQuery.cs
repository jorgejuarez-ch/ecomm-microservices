namespace Ordering.Application.Orders.Queries.GetByName
{
    public record GetOrdersByNameQuery(string Name) : IQuery<GetOrdersByNameResult>;

    public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);
}
