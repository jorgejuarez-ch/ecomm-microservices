using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {
        private readonly IBasketRepository _repository = repository;
        private readonly IDistributedCache _cache = cache;

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await _cache.GetStringAsync(userName, cancellationToken);
            ShoppingCart basket;

            if (!string.IsNullOrEmpty(cachedBasket))
            {
                basket = JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }
            else
            {
                basket = await _repository.GetBasket(userName, cancellationToken);
                await _cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
            }
           
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await _repository.StoreBasket(basket, cancellationToken);
            await _cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteBasket(userName, cancellationToken);
            await _cache.RemoveAsync(userName, cancellationToken);

            return true;
        }
    }
}
