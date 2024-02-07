using Microservices.Posts.Queries.Domain.Entities;

namespace Microservices.Posts.Queries.Api.Queries
{
    public interface IQueryHandler
    {
        Task<List<PostEntity>> HandleAsync(FindAllPostsQuery query);
        Task<List<PostEntity>> HandleAsync(FindPostByIdQuery query);
        Task<List<PostEntity>> HandleAsync(FindPostsByAuthorQuery query);
        Task<List<PostEntity>> HandleAsync(FindPostsWithCommentsQuery query);
        Task<List<PostEntity>> HandleAsync(FindPostsWithLikesQuery query);

    }
}
