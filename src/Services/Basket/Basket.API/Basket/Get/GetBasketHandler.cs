namespace Basket.API.Basket.Get
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);

    public class GetBasketQueryHandler (IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        private readonly IBasketRepository _repository = repository;

        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            ShoppingCart basket = await _repository.GetBasket(query.UserName, cancellationToken);

            return new GetBasketResult(basket);
        }
    }
}
