using Microservices.Posts.CQRS.Events;

namespace Microservices.Posts.Common.Events
{
    public class CommentRemovedEvent : BaseEvent
    {
        public CommentRemovedEvent() : base(nameof(CommentRemovedEvent))
        {
        }

        public Guid CommentId { get; set; }
    }
}
