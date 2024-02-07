using Microservices.Posts.CQRS.Queries;

namespace Microservices.Posts.Queries.Api.Queries
{
    public class FindPostsWithLikesQuery : BaseQuery
    {
        public int NumberOfLikes { get; set; }
    }
}
