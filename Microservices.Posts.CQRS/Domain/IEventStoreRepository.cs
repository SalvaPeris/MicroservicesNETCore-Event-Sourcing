using Microservices.Posts.CQRS.Events;

namespace Microservices.Posts.CQRS.Domain
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(EventModel @event);
        Task<IEnumerable<EventModel>> FindByAggregateIdAsync(Guid aggregateId);
    }
}
