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

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
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
       
        #endregion
    }
}
