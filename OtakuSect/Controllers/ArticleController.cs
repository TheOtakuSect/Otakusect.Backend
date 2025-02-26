﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.BussinessLayer.Services.Interface;
using OtakuSect.ViewModel.Request;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace OtakuSect.Controllers
{
    [Route("api/article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IAuthService _authService;

        public ArticleController(IArticleService articleService, IAuthService authService)
        {
            _articleService = articleService;
            _authService = authService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all articles, visible to every users")]
        public async Task<IActionResult> GetAllArticle()
        {
            var result = await _articleService.GetAllArticle();
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "Post article to database")]
        public async Task<IActionResult> PostArticle([FromForm] ArticleRequest articleRequest)
        {
            var userId = _authService.GetCurrentUser(HttpContext.User.Identity as ClaimsIdentity).UserId;
            var result = await _articleService.PostArticle(userId, articleRequest);
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        [SwaggerOperation(Summary = "Edit article")]
        public async Task<IActionResult> UpdateArticle([FromForm] ArticleUpdateRequest ArticleRequest)
        {
            var userId = _authService.GetCurrentUser(HttpContext.User.Identity as ClaimsIdentity).UserId;
            var result = await _articleService.UpdateArticle(userId, ArticleRequest);
            return Ok(result);
        }
    }
}
