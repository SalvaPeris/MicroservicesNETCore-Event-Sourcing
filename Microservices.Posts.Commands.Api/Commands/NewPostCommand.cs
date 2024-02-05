using Microservices.Posts.CQRS.Commands;

namespace Microservices.Posts.Commands.Api.Commands
{
    public class NewPostCommand : BaseCommand
    {
        public string? Author { get; set; }

        public string? Message { get; set; }
    }
}
