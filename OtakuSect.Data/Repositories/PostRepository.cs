using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OtakuSect.Data.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.Data.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Post> GetAllPosts()
        {
            var posts = _context.Posts
                .Include(entity => entity.Attachments)
                .Include(entity => entity.User)
                .Select(p => new Post
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    IsSafeToWatch = p.IsSafeToWatch,
                    Tags = p.Tags,
                    PostedDateTime = p.PostedDateTime,
                    TotalRate = p.TotalRate,
                    ViewCount = p.ViewCount,
                    User = new User
                    {
                        Id = p.User.Id,
                        UserName = p.User.UserName,
                        FullName = p.User.FullName,
                        EmailAddress = p.User.EmailAddress,
                    },
                }).ToList();
            return posts;
        }
    }
}
