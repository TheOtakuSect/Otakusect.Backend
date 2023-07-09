using OtakuSect.Data.Entities;
using OtakuSect.Data.GenericRepositories;
using System.Linq.Expressions;

namespace OtakuSect.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetByIdAsync(Guid pId, params Expression<Func<Post, object>>[] includes);
        Task<int> FetchViewCount(Guid pId);
    }
}
