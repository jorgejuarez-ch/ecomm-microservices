using Discount.Grpc;

namespace Basket.API.Basket.Store
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketHandler (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        private readonly IBasketRepository _repository = repository;
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient = discountProtoServiceClient;

        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscountAsync(command.Cart, cancellationToken);
            await _repository.StoreBasket(command.Cart, cancellationToken);

            return new StoreBasketResult(command.Cart.UserName);
        }

        private async Task DeductDiscountAsync(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (ShoppingCartItem item in cart.Items)
            {
                var coupon = await _discountProtoServiceClient.GetDiscountAsync(new GetDiscountRequest
                {
                    ProductName = item.ProductName
                }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }
}
