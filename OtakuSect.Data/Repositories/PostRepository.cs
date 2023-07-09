using Microsoft.EntityFrameworkCore;
using OtakuSect.Data.Context;
using OtakuSect.Data.Entities;
using OtakuSect.Data.GenericRepositories;
using System.Linq.Expressions;

namespace OtakuSect.Data.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<int> FetchViewCount(Guid pId)
        {
            var post = await _context.Posts.FindAsync(pId);
            int views = post.ViewCount ?? 0;
            return views;
        }

        public async Task<Post> GetByIdAsync(Guid Id, params Expression<Func<Post, object>>[] includes)
        {
            IQueryable<Post> query = _context.Set<Post>();

            // Apply any included navigation properties to the query
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Execute the query to retrieve the entity by its Id
            Post entity = await query.FirstOrDefaultAsync(e => e.Id == Id);

            return entity;
        }
    }
}
