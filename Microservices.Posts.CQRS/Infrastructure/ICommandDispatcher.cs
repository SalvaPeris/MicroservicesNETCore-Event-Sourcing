using Microservices.Posts.CQRS.Commands;

namespace Microservices.Posts.CQRS.Infrastructure
{
    public interface ICommandDispatcher
    {
        void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand;
        Task SendAsync(BaseCommand command);
    }
}
