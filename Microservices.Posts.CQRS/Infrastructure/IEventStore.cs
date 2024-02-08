using Microservices.Posts.CQRS.Events;

namespace Microservices.Posts.CQRS.Infrastructure
{
    public interface IEventStore
    {
        Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion);
        Task<ICollection<BaseEvent?>> GetEventsAsync(Guid aggregateId);
        Task<List<Guid>> GetAggregateIdsAsync();
    }
}
