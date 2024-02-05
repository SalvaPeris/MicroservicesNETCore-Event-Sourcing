namespace Microservices.Posts.Commands.Api.Commands
{
    public interface ICommandHandler
    {
        Task HandleAsync(NewPostCommand comand);
        Task HandleAsync(EditMessageCommand comand);
        Task HandleAsync(LikePostCommand comand);
        Task HandleAsync(AddCommentCommand comand);
        Task HandleAsync(EditCommentCommand comand);
        Task HandleAsync(RemoveCommentCommand comand);
        Task HandleAsync(DeletePostCommand comand);
    }
}
