using OtakuSect.Data;
using OtakuSect.ViewModel.Request;

namespace OtakuSect.BussinessLayer
{
    public interface IArticleService
    {
        public Task<ApiResponse<Article>> PostArticle(Guid uId, ArticleViewModel articleViewModel);
        public ApiResponse<string> DeleteArticle(Guid id);
        public Task<ApiResponse<Article>> GetArticleById(Guid id);
        public IEnumerable<Article> GetAllArticle();

        public Task<ApiResponse<Article>> UpdateArticle(Guid userId,Guid id, ArticleViewModel articleViewModel);
    }
}
