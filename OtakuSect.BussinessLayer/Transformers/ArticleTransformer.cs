using OtakuSect.Data.Entities;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer.Transformers
{
    public static class ArticleTransformer
    {
        public static List<ArticleResponse> GetArticleResponseFromArticle(List<Article> articles)
        {
            var articleResponses = new List<ArticleResponse>();
            articles.ForEach((article) =>
            {
                var users = new List<ArticleUser>();
                article.UserArticles?.ForEach((user) =>
                {
                    users.Add(new ArticleUser { Id = user.UserId, UserName = user.User?.UserName });
                });
                var articleResponse = new ArticleResponse()
                {
                    Id = article.Id,
                    Title = article.Title,
                    Description = article.Description,
                    ViewCount = article.ViewCount,
                    Categories = article.Categories?.Select(x => x.Name).ToList(),
                    Attachments = article.Attachments?.Select(x => x.Name).ToList(),
                    Users = users
                };
                articleResponses.Add(articleResponse);
            });
            return articleResponses;
        }

        public static ArticleResponse GetArticleResponseFromArticle(Article article)
        {
            var users = new List<ArticleUser>();
            article?.UserArticles?.ForEach((user) =>
            {
                users.Add(new ArticleUser { Id = user.UserId, UserName = user.User?.UserName });
            });
            var articleResponse = new ArticleResponse();
            articleResponse.Id = article.Id;
            articleResponse.Title = article?.Title;
            articleResponse.Description = article?.Description;
            articleResponse.ViewCount = article.ViewCount;
            articleResponse.Categories = article.Categories?.Select(x => x.Name).ToList();
            articleResponse.Attachments = article.Attachments?.Select(x => x.Name).ToList();
            articleResponse.Users = users;

            return articleResponse;
        }
    }
}
