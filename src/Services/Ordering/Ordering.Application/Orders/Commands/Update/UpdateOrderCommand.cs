namespace Ordering.Application.Orders.Commands.Update
{
    public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

    public record UpdateOrderResult(bool IsSuccedd);
    
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.Order.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(o => o.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(o => o.Order.CustomerId).NotNull().WithMessage("CustomerId is requiered");
        }
    }
}
