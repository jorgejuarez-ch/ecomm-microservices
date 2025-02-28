namespace Shopping.Web.Models.Ordering
{
    public record GetOrdersByCustomerResponse(IEnumerable<OrderModel> Orders);
}
