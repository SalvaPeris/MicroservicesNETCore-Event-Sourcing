namespace Microservices.Posts.Commands.Api.Commands
{
    public interface ICommandHandler
    {
        Task HandleAsync(NewPostCommand comand);
        Task HandleAsync(EditMessageCommand comand);
        Task HandleAsync(AddLikePostCommand comand);
        Task HandleAsync(RemoveLikePostCommand comand);
        Task HandleAsync(AddCommentCommand comand);
        Task HandleAsync(EditCommentCommand comand);
        Task HandleAsync(RemoveCommentCommand comand);
        Task HandleAsync(DeletePostCommand comand);
        Task HandleAsync(RestoreReadDbCommand command);

    }
}
