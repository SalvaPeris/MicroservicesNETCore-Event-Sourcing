using Microservices.Posts.CQRS.Commands;

namespace Microservices.Posts.Commands.Api.Commands
{
    public class EditCommentCommand : BaseCommand
    {
        public Guid CommentId { get; set; }
        public string? Comment { get; set; }

        public string? Username { get; set; }
    }
}
