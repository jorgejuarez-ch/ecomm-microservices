
namespace Ordering.Application.Orders.Commands.Delete
{


    public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        private readonly IApplicationDbContext _dbContext = dbContext;

        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            OrderId orderId = OrderId.Of(command.OrderId);
            Order? order = await _dbContext.Orders.FindAsync([orderId], cancellationToken) ?? throw new OrderNotFoundException(command.OrderId);
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);
        }
    }
}
