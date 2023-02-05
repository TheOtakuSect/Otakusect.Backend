using OtakuSect.Data.Entities;
using OtakuSect.Data.GenericRepositories;

namespace OtakuSect.Data.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {
        public Task<UserArticle> PostWithUser(UserArticle userArticle);
    }
}
