namespace Ordering.Application.Orders.Commands.Delete
{
    public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;
    
    public record DeleteOrderResult(bool IsSuccess);

    public class DeleteOrderCommandValidatior : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidatior()
        {
            RuleFor(o => o.OrderId).NotEmpty().WithMessage("OrderId is required");
        }
    }
}
