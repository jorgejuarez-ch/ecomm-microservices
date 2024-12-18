namespace Ordering.Domain.Events
{
    public record OrderCreatedEvent(Order rorder) : IDomainEvent;
}
