using Microservices.Posts.CQRS.Domain;

namespace Microservices.Posts.CQRS.Handlers
{
    public interface IEventSourcingHandler<T>
    {
        Task SaveAsync(AggregateRoot aggregate);
        Task<T> GetByIdAsync(Guid aggregateId);
        Task RepublishEventsAsync();
    }
}
