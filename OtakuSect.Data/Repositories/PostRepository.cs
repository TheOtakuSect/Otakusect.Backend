using OtakuSect.Data.Context;
using OtakuSect.Data.Entities;
using OtakuSect.Data.GenericRepositories;

namespace OtakuSect.Data.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }
    }
}
