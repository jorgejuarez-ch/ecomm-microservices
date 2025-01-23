namespace Ordering.Application.Orders.Commands.Create
{
    public class CreateOrderHandler(IApplicationDbContext dbContext)
        : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        private readonly IApplicationDbContext _dbContext = dbContext;

        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = CreateNewOrder(command.Order);

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Value);
        }

        private Order CreateNewOrder(OrderDto orderDto)
        {
            Address shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName,
                orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country,
                orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
            Address billingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName,
                orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country,
                orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

            Order newOrder = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(orderDto.CustomerId), OrderName.Of(orderDto.OrderName),
                shippingAddress, billingAddress, Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber,
                orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod));

            return newOrder;
        }
    }
}
