using Microsoft.EntityFrameworkCore;
using OtakuSect.Data.Context;
using OtakuSect.Data.Entities;
using OtakuSect.Data.GenericRepositories;

namespace OtakuSect.Data.Repositories
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(AppDbContext options) : base(options) { }

        public IEnumerable<Article> GetAllArticles()
        {
            var articles = _context.Articles
                .Include(a => a.Attachments)
                .Include(a => a.Comments)
                .Include(a => a.UserArticles)
                .Select(a => new Article
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    ViewCount = a.ViewCount,
                    UserArticles = a.UserArticles.Select(ua => new UserArticle
                    {
                        UserId = ua.UserId
                    }).ToList()
                });
            return articles;
        }

        public async Task<UserArticle> PostWithUser(UserArticle userArticle)
        {
            await _context.UsersArticles.AddAsync(userArticle);
            _context.SaveChanges();
            return userArticle;
        }
    }
}
