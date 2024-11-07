namespace Basket.API.Basket.Store
{
    public class StoreCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreCommandValidator()
        {
            RuleFor(v => v.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(v => v.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
}
