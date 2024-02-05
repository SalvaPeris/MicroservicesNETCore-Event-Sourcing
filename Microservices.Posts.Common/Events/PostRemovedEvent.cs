using Microservices.Posts.CQRS.Events;

namespace Microservices.Posts.Common.Events
{
    public class PostRemovedEvent : BaseEvent
    {
        public PostRemovedEvent() : base(nameof(PostRemovedEvent))
        {
        }

        public Guid CommentId { get; set; }
    }
}
