using OtakuSect.Data;
using OtakuSect.ViewModel;

namespace OtakuSect.BussinessLayer
{
    public interface IArticleService
    {
        //public ApiResponse<Article> PostArticle(List<User> users , ArticleViewModel articleViewModel);
        public ApiResponse<string> DeleteArticle(Guid id);
        public Task<ApiResponse<Article>> GetArticleById(Guid id);
        public Task<ApiResponse<Article>> EditArticle(Guid id, ArticleViewModel articleViewModel);
    }
}
