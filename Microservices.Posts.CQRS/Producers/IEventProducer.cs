using Microservices.Posts.CQRS.Events;

namespace Microservices.Posts.CQRS.Producers
{
    public interface IEventProducer
    {
        Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent;
    }
}
