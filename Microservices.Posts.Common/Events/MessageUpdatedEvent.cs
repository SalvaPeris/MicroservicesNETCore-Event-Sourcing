using Microservices.Posts.CQRS.Events;

namespace Microservices.Posts.Common.Events
{
    public class MessageUpdatedEvent : BaseEvent
    {
        public MessageUpdatedEvent() : base(nameof(MessageUpdatedEvent))
        {
        }

        public string? Message { get; set; }
    }
}
