using Microservices.Posts.CQRS.Commands;

namespace Microservices.Posts.Commands.Api.Commands
{
    public class DeletePostCommand : BaseCommand
    {
        public string? Username { get; set; }
    }
}
