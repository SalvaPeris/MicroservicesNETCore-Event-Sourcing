namespace Microservices.Posts.CQRS.Messages
{
    public abstract class Message
    {
        public Guid Id { get; set; }
    }
}
