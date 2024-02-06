using Microservices.Posts.Queries.Domain.Entities;

namespace Microservices.Posts.Queries.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task CreateAsync(CommentEntity comment);
        Task UpdateAsync(CommentEntity comment);
        Task<CommentEntity?> GetByIdAsync(Guid commentId);
        Task DeleteAsync(Guid commentId);
    }
}
