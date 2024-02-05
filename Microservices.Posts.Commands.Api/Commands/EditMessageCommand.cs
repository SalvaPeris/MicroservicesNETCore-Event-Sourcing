using Microservices.Posts.CQRS.Commands;

namespace Microservices.Posts.Commands.Api.Commands
{
    public class EditMessageCommand : BaseCommand
    {
        public string? Message { get; set; }
    }
}
