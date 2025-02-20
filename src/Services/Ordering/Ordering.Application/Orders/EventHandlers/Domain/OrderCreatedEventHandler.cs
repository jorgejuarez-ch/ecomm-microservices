using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger, IPublishEndpoint publishEndpoint,
        IFeatureManager featureManager) : INotificationHandler<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedEventHandler> _logger = logger;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IFeatureManager _featureManager = featureManager;

        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

            if (await _featureManager.IsEnabledAsync("OrderFullfillment"))
            {
                var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();
                await _publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }
        }
    }
}
