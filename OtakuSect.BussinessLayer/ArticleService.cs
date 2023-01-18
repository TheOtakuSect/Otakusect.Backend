using Microsoft.AspNetCore.SignalR;
using OtakuSect.Data;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.BussinessLayer
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IAttachmentService _attachmentService;

        public ArticleService(IArticleRepository articleRepository,IAttachmentService attachmentService)
        {
            _articleRepository = articleRepository;
            _attachmentService = attachmentService;
        }
        #region Delete Article
        public ApiResponse<string> DeleteArticle(Guid id)
        {
            var apiResponse = new ApiResponse<string>();
            try
            {
                _articleRepository.DeleteAsync(id);
                apiResponse.Success = true;
                apiResponse.StatusCode= 200;
                apiResponse.Message = "Article deleted";
                return apiResponse;
            }
            catch(Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.StatusCode= 500;
                apiResponse.Message = ex.Message;
                return apiResponse;
            }
        }

        public Task<ApiResponse<Article>> EditArticle(Guid id, ArticleViewModel articleViewModel)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Get article
        public async Task<ApiResponse<Article>> GetArticleById(Guid id)
        {
            var apiResponse = new ApiResponse<Article>();
            try
            {
                var article = await _articleRepository.GetByIdAsync(id);
                apiResponse.StatusCode= 200;
                apiResponse.Success = true;
                apiResponse.Data= article;
                return apiResponse;
            }
            catch(Exception ex) 
            { 
                apiResponse.Success = false;
                apiResponse.StatusCode= 500;
                apiResponse.Message = ex.Message;
                return apiResponse;
            }
        }
        #endregion

        #region Post article
        public async Task<ApiResponse<Article>> PostArticle(Guid uId, ArticleViewModel articleViewModel)
        {
            var apiResponse = new ApiResponse<Article>();
            try
            {
                var article = new Article
                {
                    Id = Guid.NewGuid(),
                    Title = articleViewModel.Title,
                    Description = articleViewModel.Description,
                    Attachments = _attachmentService.UploadFile(articleViewModel.Files)
                };
                var userArticles = new UserArticle()
                {
                    UserId = uId,
                    ArticleId = article.Id,
                };
                await _articleRepository.AddAsync(article);
                apiResponse.StatusCode= 200;
                apiResponse.Success = true;
                apiResponse.Data= article;
                return apiResponse;
            }
            catch(Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.StatusCode= 500;
                apiResponse.Message = ex.Message;
                return apiResponse;
            }
        }
        #endregion
    }
}
