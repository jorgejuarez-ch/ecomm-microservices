namespace Basket.API.Basket.Delete
{
    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(v => v.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
}
