using Microsoft.EntityFrameworkCore;
using OtakuSect.Data.Context;
using OtakuSect.Data.Entities;
using OtakuSect.Data.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.Data.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext options) : base(options)
        {
            
        }
        public async Task<Comment> GetByIdAsync(Guid Id, params Expression<Func<Comment, object>>[] includes)
        {
            IQueryable<Comment> query = _context.Set<Comment>();

            // Apply any included navigation properties to the query
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Execute the query to retrieve the entity by its Id
            Comment entity = await query.FirstOrDefaultAsync(e => e.Id == Id);

            return entity;
        }
        public async Task<IEnumerable<Comment>> GetByPostId(Guid Id,params Expression<Func<Comment, object>>[] includes)
        {
            IQueryable<Comment> query = _context.Comments;

            // Apply the includes
            if (includes != null && includes.Length > 0)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            // Filter comments by PostId
            query = query.Where(comment => comment.PostId == Id);

            // Execute the query and return the comments
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Comment>> GetByArticleId(Guid Id, params Expression<Func<Comment,object>>[] includes)
        {

            IQueryable<Comment> query = _context.Comments;

            // Apply the includes
            if (includes != null && includes.Length > 0)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            // Filter comments by PostId
            query = query.Where(comment => comment.ArticleId == Id);

            // Execute the query and return the comments
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetByUserId(Guid Id, params Expression<Func<Comment, object>>[] includes)
        {
            IQueryable<Comment> query = _context.Comments;

            // Apply the includes
            if (includes != null && includes.Length > 0)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            // Filter comments by PostId
            query = query.Where(comment => comment.UserId == Id);

            // Execute the query and return the comments
            return await query.ToListAsync();
        }

        public  async Task<List<Comment>> GetReplyIdAsync(Guid Id, params Expression<Func<Comment, object>>[] includes)
        {
            IQueryable<Comment> query = _context.Comments;
            if (includes != null && includes.Length > 0)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            query = query.Where(comment => comment.ParentCommentId == Id);

            return await query.ToListAsync();
        }

       
    }
}
