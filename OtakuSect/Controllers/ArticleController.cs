using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.BussinessLayer;
using OtakuSect.ViewModel.Request;
using System.Security.Claims;

namespace OtakuSect.Controllers
{
    [Route("api/article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IAuthService _authService;

        public ArticleController(IArticleService articleService,IAuthService authService)
        {
            _articleService = articleService;
            _authService = authService;
        }

        #region Post article 
        [HttpPost]
        public async Task<IActionResult> PostArticle([FromForm]ArticleViewModel articleViewModel )
        {
            var userid = _authService.GetCurrentUser(HttpContext.User.Identity as ClaimsIdentity).UserId;
            var result =  await _articleService.PostArticle(userid, articleViewModel);
            return Ok(result);
        }
        #endregion

        #region update article 
        [HttpPost("edit-article/{id}")]
        [Authorize("SectElder")]
        public async Task<IActionResult> UpdateArticle(Guid id,[FromForm] ArticleViewModel articleViewModel)
        {
            var userid = _authService.GetCurrentUser(HttpContext.User.Identity as ClaimsIdentity).UserId;
            var result = await _articleService.UpdateArticle(userid,id, articleViewModel);
            return Ok(result);
        }
        #endregion


        #region Get all article 
        [HttpGet]
        public IActionResult GetAllArticle()
        {
            var result =_articleService.GetAllArticle();
            return Ok(result);
        }
        #endregion

    }
}
