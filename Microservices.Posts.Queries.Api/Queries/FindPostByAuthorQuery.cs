using Microservices.Posts.CQRS.Queries;

namespace Microservices.Posts.Queries.Api.Queries
{
    public class FindPostByAuthorQuery : BaseQuery
    {
        public string Author{ get; set; }
    }
}
