using Microservices.Posts.CQRS.Queries;

namespace Microservices.Posts.CQRS.Infrastructure
{
    public interface IQueryDispatcher<TEntity> 
    {
        void RegisterHandler<TQuery>(Func<TQuery, Task<List<TEntity>>> handler) where TQuery : BaseQuery;
        Task<List<TEntity>> SendAsync(BaseQuery query);
    }
}
