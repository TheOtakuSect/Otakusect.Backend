using OtakuSect.ViewModel.Request;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer.Services.Interface
{
    public interface IArticleService
    {
        public Task<ApiResponse<List<ArticleResponse>>> GetAllArticle();
        public Task<ApiResponse<ArticleResponse>> GetArticleById(Guid id);
        public Task<ApiResponse<ArticleResponse>> PostArticle(Guid userId, ArticleRequest articleRequest);
        public Task<ArticleResponse> TestArticle(Guid userId, ArticleRequest articleRequest);

        public Task<ApiResponse<ArticleResponse>> UpdateArticle(Guid userId, ArticleUpdateRequest articleRequest);
        public Task<ApiResponse<string>> DeleteArticle(Guid id);
    }
}
