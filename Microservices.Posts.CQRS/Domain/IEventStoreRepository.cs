using Microservices.Posts.CQRS.Events;

namespace Microservices.Posts.CQRS.Domain
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(EventModel @event);
        Task<ICollection<EventModel>> FindByAggregateIdAsync(Guid aggregateId);
        Task<List<EventModel>> FindAllAsync();
    }
}
