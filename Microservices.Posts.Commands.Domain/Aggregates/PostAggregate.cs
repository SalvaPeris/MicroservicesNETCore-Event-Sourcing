using Microservices.Posts.Common.Events;
using Microservices.Posts.CQRS.Domain;
using ZstdSharp.Unsafe;

namespace Microservices.Posts.Commands.Domain.Aggregates
{
    public class PostAggregate : AggregateRoot
    {
        private bool _active;
        private int _likes;
        private string? _author;
        private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();

        public bool Active 
        {
            get => _active; set => _active = value;
        }

        public int Likes
        {
            get => _likes; set => _likes = value;
        }

        public PostAggregate()
        {

        }

        public PostAggregate(Guid id, string author, string message)
        {
            RaiseEvent(new PostCreatedEvent
            {
                Id = id,
                Author = author,
                Message = message,
                DatePosted = DateTime.Now
            });
        }

        public void Apply(PostCreatedEvent @event)
        {
            _active = true;
            _id = @event.Id;
            _author = @event.Author;
        }

        public void EditMessage(string message)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot edit the message of inactive post");
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new InvalidOperationException($"The value of {nameof(message)} cannot be null or empty.");
            }

            RaiseEvent(new MessageUpdatedEvent { Id = _id, Message = message });
        }

        public void Apply(MessageUpdatedEvent @event)
        {
            _id = @event.Id;
        }

        public void AddLikePost()
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot like an inactive post");
            }

            RaiseEvent(new PostLikedEvent { Id = _id });
        }

        public void Apply(PostLikedEvent @event)
        {
            _id = @event.Id;
        }

        public void RemoveLikePost()
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot like an inactive post");
            }

            if (_likes <= 0)
            {
                throw new InvalidOperationException("You cannot remove like in post with empty likes");
            }

            RaiseEvent(new PostNotLikedEvent { Id = _id });
        }

        public void Apply(PostNotLikedEvent @event)
        {
            _id = @event.Id;
        }

        public void AddComment(string comment, string username)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot add comments to an inactive post");
            }

            if (string.IsNullOrWhiteSpace(comment))
            {
                throw new InvalidOperationException($"The value of {nameof(comment)} cannot be null or empty.");
            }

            RaiseEvent(new CommentAddedEvent { Id = _id, CommentId = Guid.NewGuid(), Comment = comment, Username = username, CommentDate = DateTime.Now });
        }

        public void Apply(CommentAddedEvent @event)
        {
            _id = @event.Id;
            _comments.Add(@event.CommentId, new Tuple<string, string>(@event.Comment, @event.Username));
        }

        public void EditComment(Guid commentId, string comment, string username)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot edit a comment to an inactive post");
            }

            if (!_comments[commentId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidOperationException($"You are not allowed to edit a {nameof(comment)} by another user.");
            }

            RaiseEvent(new CommentUpdatedEvent { Id = _id, CommentId = commentId, Comment = comment, Username = username, EditDate = DateTime.Now });
        }

        public void Apply(CommentUpdatedEvent @event)
        {
            _id = @event.Id;
            _comments[@event.CommentId] = new Tuple<string, string>(@event.Comment, @event.Username);
        }

        public void RemoveComment(Guid commentId, string username)
        {
            if (!_active)
            {
                throw new InvalidOperationException("You cannot remove a comment to an inactive post");
            }

            if (!_comments[commentId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidOperationException($"You are not allowed to remove a comment by another user.");
            }

            RaiseEvent(new CommentRemovedEvent { Id = _id, CommentId = commentId });
        }

        public void Apply(CommentRemovedEvent @event)
        {
            _id = @event.Id;
            _comments.Remove(@event.CommentId);
        }

        public void DeletePost(string username)
        {
            if (!_active)
            {
                throw new InvalidOperationException("The post has already removed");
            }

            if(!_author.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new InvalidOperationException($"You are not allowed to delete a post that was made by another user.");
            }

            RaiseEvent(new PostRemovedEvent { Id = _id });
        }

        public void Apply(PostRemovedEvent @event)
        {
            _id = @event.Id;
            _active = false;
        }
    }
}
