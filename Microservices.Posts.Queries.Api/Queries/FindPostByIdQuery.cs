using Microservices.Posts.CQRS.Queries;

namespace Microservices.Posts.Queries.Api.Queries
{
    public class FindPostByIdQuery : BaseQuery
    {
        public Guid Id { get; set; }
    }
}
