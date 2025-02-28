namespace Shopping.Web.Models.Ordering
{
    public record OrderModel(Guid Id, Guid CustomerId, string OrderName, AddressModel ShippingAddress, AddressModel BillingAddress,
        PaymentModel Payment, OrderStatus Status, List<OrderItemModel> OrderItems);

    public enum OrderStatus
    {
        Draft = 1,
        Pending = 2,
        Completed = 3,
        Cancelled = 4
    }
}
