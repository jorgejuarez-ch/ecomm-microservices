﻿using Basket.API.Exceptions;

namespace Basket.API.Data
{
    public class BasketRepository (IDocumentSession session) : IBasketRepository
    {
        private readonly IDocumentSession _session = session;

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await _session.LoadAsync<ShoppingCart>(userName, cancellationToken);

            return basket is null ? throw new BasketNotFoundException(userName) : basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            _session.Store(basket);
            await _session.SaveChangesAsync(cancellationToken);

            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            _session.Delete<ShoppingCart>(userName);
            await _session.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
