using Microservices.Posts.CQRS.Events;

namespace Microservices.Posts.Common.Events
{
    public class PostNotLikedEvent : BaseEvent
    {
        public PostNotLikedEvent() : base(nameof(PostNotLikedEvent))
        {
        }
    }
}
