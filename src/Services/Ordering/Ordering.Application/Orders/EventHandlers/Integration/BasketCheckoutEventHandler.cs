﻿using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.Create;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler (ISender sender, ILogger<BasketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
    {
        private readonly ISender _sender = sender;
        private readonly ILogger<BasketCheckoutEventHandler> _logger = logger;

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            _logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

            var command = MapToCreateOrderCommand(context.Message);
            await sender.Send(command);
        }

        private static CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
            var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country,
                message.State, message.ZipCode);
            var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
            var orderId = Guid.NewGuid();

            var orderDto = new OrderDto(orderId, message.CustomerId, message.UserName, addressDto, addressDto, paymentDto, Ordering.Domain.Enums.OrderStatus.Pending,
                [
                    new OrderItemDto(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500),
                    new OrderItemDto(orderId, new Guid("c67d6323-e8b1-4dbf-9a75-b0d0d2e7e914"), 1, 400)
                ]);

            return new CreateOrderCommand(orderDto);
        }
    }
}
