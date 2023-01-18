using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.BussinessLayer;
using OtakuSect.ViewModel;
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
    }
}
