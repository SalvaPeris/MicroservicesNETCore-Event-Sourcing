namespace Microservices.Posts.CQRS.Consumers
{
    public interface IEventConsumer
    {
        void Consume(string topic);
    }
}
