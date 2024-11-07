namespace Basket.API.Basket.Store
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketHandler (IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        private readonly IBasketRepository _repository = repository;

        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;
            await _repository.StoreBasket(cart, cancellationToken);

            return new StoreBasketResult(command.Cart.UserName);
        }
    }
}
