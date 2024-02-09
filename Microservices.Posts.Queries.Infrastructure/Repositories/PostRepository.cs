using Microservices.Posts.Queries.Domain.Entities;
using Microservices.Posts.Queries.Domain.Repositories;
using Microservices.Posts.Queries.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Posts.Queries.Infrastructure.Repositories
{
    public class PostRepository(DatabaseContextFactory contextFactory) : IPostRepository
    {
        private readonly DatabaseContextFactory _contextFactory = contextFactory;

        public async Task CreateAsync(PostEntity post)
        {
            try
            {
                using DatabaseContext context = _contextFactory.CreateDbContext();
                await context.Posts.AddAsync(post);

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public async Task DeleteAsync(Guid postId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            var post = await GetByIdAsync(postId);

            if (post == null) return;

            context.Posts.Remove(post);
            await context.SaveChangesAsync();
        }

        public async Task<PostEntity> GetByIdAsync(Guid postId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts
                    .Include(p => p.Comments)
                    .FirstOrDefaultAsync(x => x.PostId == postId);
        }

        public async Task<List<PostEntity>> ListAllAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            var posts = await context.Posts.AsNoTracking()
                    .Include(p => p.Comments).AsNoTracking()
                    .ToListAsync();

            return posts;
        }

        public async Task<List<PostEntity>> ListByAuthorAsync(string author)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts.AsNoTracking()
                    .Include(p => p.Comments).AsNoTracking()
                    .Where(x => x.Author.Contains(author))
                    .ToListAsync();
        }

        public async Task<List<PostEntity>> ListWithCommentsAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts.AsNoTracking()
                    .Include(p => p.Comments).AsNoTracking()
                    .Where(x => x.Comments != null && x.Comments.Count() > 0)
                    .ToListAsync();
        }

        public async Task<List<PostEntity>> ListWithLikesAsync(int numberOfLikes)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts.AsNoTracking()
                    .Include(p => p.Comments).AsNoTracking()
                    .Where(x => x.Likes >= numberOfLikes)
                    .ToListAsync();
        }

        public async Task UpdateAsync(PostEntity post)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Posts.Update(post);

            await context.SaveChangesAsync();
        }
    }
}
