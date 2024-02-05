using Microservices.Posts.CQRS.Events;

namespace Microservices.Posts.Common.Events
{
    public class PostLikedEvent : BaseEvent
    {
        public PostLikedEvent() : base(nameof(PostLikedEvent))
        {
        }
    }
}
