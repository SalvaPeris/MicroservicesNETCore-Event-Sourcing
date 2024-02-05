using Microservices.Posts.CQRS.Commands;

namespace Microservices.Posts.Commands.Api.Commands
{
    public class AddCommentCommand : BaseCommand
    {
        public string? Comment {  get; set; }
        public string? Username { get; set; }
    }
}
