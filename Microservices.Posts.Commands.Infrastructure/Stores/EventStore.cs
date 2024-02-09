using Microservices.Posts.Commands.Domain.Aggregates;
using Microservices.Posts.CQRS.Domain;
using Microservices.Posts.CQRS.Events;
using Microservices.Posts.CQRS.Exceptions;
using Microservices.Posts.CQRS.Infrastructure;
using Microservices.Posts.CQRS.Producers;

namespace Microservices.Posts.Commands.Infrastructure.Stores
{
    public class EventStore(IEventStoreRepository eventStoreRepository, IEventProducer eventProducer) : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository = eventStoreRepository;
        private readonly IEventProducer _eventProducer = eventProducer;

        public async Task<List<Guid>> GetAggregateIdsAsync()
        {
            var eventStream = await _eventStoreRepository.FindAllAsync();

            if (eventStream == null || eventStream.Count == 0)
                throw new ArgumentNullException(nameof(eventStream), "Could not retrieve event stream from the event store!");

            return eventStream.Select(x => x.AggregateIdentifier).Distinct().ToList();
        }

        public async Task<ICollection<BaseEvent?>> GetEventsAsync(Guid aggregateId)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateIdAsync(aggregateId);

            if(eventStream == null)
                throw new AggregateNotFoundException("Incorrect post id provided.");

            return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
        }

        public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
        {
            var eventStream = (List<EventModel>) await _eventStoreRepository.FindByAggregateIdAsync(aggregateId);

            if(expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
                throw new ConcurrencyException();
            
            var version = expectedVersion;
            foreach(var @event in events)
            {
                version++;
                @event.Version = version;
                var eventType = @event.GetType().Name;
                var eventModel = new EventModel
                {
                    TimeStamp = DateTime.Now,
                    AggregateIdentifier = aggregateId,
                    AggregateType = nameof(PostAggregate),
                    Version = version,
                    EventType = eventType,
                    EventData = @event
                };

                await _eventStoreRepository.SaveAsync(eventModel);

                var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
                await _eventProducer.ProduceAsync(topic, @event);
            }

        }
    }
}
