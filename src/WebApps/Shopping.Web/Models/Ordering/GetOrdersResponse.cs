namespace Shopping.Web.Models.Ordering
{
    public record GetOrdersResponse(PaginatedResult<OrderModel> Orders);
}
