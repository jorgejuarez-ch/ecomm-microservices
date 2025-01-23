namespace Ordering.Application.Orders.Queries.Get
{
    public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>;

    public record GetOrdersResult(PaginatedResult<OrderDto> Orders);
}
