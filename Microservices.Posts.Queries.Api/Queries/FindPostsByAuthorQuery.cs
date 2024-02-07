using Microservices.Posts.CQRS.Queries;

namespace Microservices.Posts.Queries.Api.Queries
{
    public class FindPostsByAuthorQuery : BaseQuery
    {
        public string Author{ get; set; }
    }
}
