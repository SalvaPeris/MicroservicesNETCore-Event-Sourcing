using Microservices.Posts.Queries.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Posts.Queries.Infrastructure.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options): base(options) { }

        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
    }
}
