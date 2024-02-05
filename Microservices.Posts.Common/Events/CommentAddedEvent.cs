using Microservices.Posts.CQRS.Events;

namespace Microservices.Posts.Common.Events
{
    public class CommentAddedEvent : BaseEvent
    {
        public CommentAddedEvent() : base(nameof(CommentAddedEvent))
        {
        }

        public string? CommentId { get; set; }
        public string? Comment { get; set; }
        public string? Username { get; set; }
        public DateTime? CommentDate { get; set; }
    }
}
