using Microservices.Posts.CQRS.Messages;

namespace Microservices.Posts.CQRS.Events
{
    public abstract class BaseEvent : Message
    {
        protected BaseEvent(string? type)
        {
            this.Type = type;
        }

        public int Version { get; set; }
        public string? Type { get; set; }
    }
}
