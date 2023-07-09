using Azure;
using OtakuSect.BussinessLayer.Helper;
using OtakuSect.BussinessLayer.Services.Interface;
using OtakuSect.BussinessLayer.Transformers;
using OtakuSect.Data.Entities;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel.Request;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepo;
        private readonly IAttachmentService _attachmentService;

        public ArticleService(IArticleRepository articleRepo, IAttachmentService attachmentService)
        {
            _articleRepo = articleRepo;
            _attachmentService = attachmentService;
        }

        public async Task<ApiResponse<List<ArticleResponse>>> GetAllArticle()
        {
            try
            {
                var result = await _articleRepo.GetAllAsync(x => x.Attachments, x => x.Categories,x=>x.UserArticles);
                var response = ArticleTransformer.GetArticleResponseFromArticle(result.ToList());
                return ResponseCreater<List<ArticleResponse>>.CreateSuccessResponse(response, "Articles loaded successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<List<ArticleResponse>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<ApiResponse<ArticleResponse>> GetArticleById(Guid id)
        {
            try
            {
                var article = await _articleRepo.GetByIdAsync(id);
                var response = ArticleTransformer.GetArticleResponseFromArticle(article);
                return ResponseCreater<ArticleResponse>.CreateSuccessResponse(response, "Article loaded successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<ArticleResponse>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<ApiResponse<ArticleResponse>> PostArticle(Guid uId, ArticleRequest articleRequest)
        {
            try
            {
                var article = new Article
                {
                    Id = Guid.NewGuid(),
                    Title = articleRequest.Title,
                    Description = articleRequest.Description,
                    Attachments = articleRequest.Files?.Count > 0 ? _attachmentService.UploadFile(articleRequest.Files) : new List<Attachment>()
                };
                var userArticle = new UserArticle
                {
                    Id = Guid.NewGuid(),
                    Article = article,
                    UserId = uId
                };
                await _articleRepo.PostWithUser(userArticle);
                var response = ArticleTransformer.GetArticleResponseFromArticle(article);
                return ResponseCreater<ArticleResponse>.CreateSuccessResponse(response, "Article posted successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<ArticleResponse>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<ApiResponse<ArticleResponse>> UpdateArticle(Guid userId, ArticleUpdateRequest articleRequest)
        {
            try
            {
                var article = await _articleRepo.GetByIdAsync(articleRequest.Id);
                article.Title = articleRequest.Title;
                article.Description = articleRequest.Description;
                article.Attachments = articleRequest.Files?.Count > 0 ? _attachmentService.UploadFile(articleRequest.Files) : new List<Attachment>();
                article.UserArticles?.Add(new UserArticle
                {
                    UserId = userId,
                    ArticleId = article.Id
                });
                _articleRepo.UpdateAsync(article);

                var response = ArticleTransformer.GetArticleResponseFromArticle(article);
                return ResponseCreater<ArticleResponse>.CreateSuccessResponse(response, "Article updated successfully.");
            }
            catch (Exception ex)
            {
                return ResponseCreater<ArticleResponse>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<ApiResponse<string>> DeleteArticle(Guid id)
        {
            try
            {
                await _articleRepo.DeleteAsync(id);
                return ResponseCreater<string>.CreateSuccessResponse(null, "Article deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<ArticleResponse> TestArticle(Guid userId, ArticleRequest articleRequest)
        {
            var article = new Article
            {
                Id = Guid.NewGuid(),
                Title = articleRequest.Title,
                Description = articleRequest.Description,
                Attachments = articleRequest.Files?.Count > 0 ? _attachmentService.UploadFile(articleRequest.Files) : new List<Attachment>()
            };
            var userArticle = new UserArticle
            {
                Id = Guid.NewGuid(),
                Article = article,
                UserId = userId
            };
            await _articleRepo.PostWithUser(userArticle);
            var response = ArticleTransformer.GetArticleResponseFromArticle(article);
            return response;

        }
    }
}
