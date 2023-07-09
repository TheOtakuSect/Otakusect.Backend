using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OtakuSect.BussinessLayer.Services.Implementations;
using OtakuSect.BussinessLayer.Services.Interface;
using OtakuSect.ViewModel.Request;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace OtakuSect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IAuthService _authService;
        public CommentsController(ICommentService commentService, IAuthService authService)
        {
            _commentService = commentService;
            _authService = authService;

        }
        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = ("create comment"))]
        public async Task<IActionResult> CreateComment([FromQuery] Guid postId, [FromForm] CommentRequest commentRequest)
        {
            var uId = _authService.GetCurrentUser(HttpContext.User.Identity as ClaimsIdentity).UserId;

            var response = await _commentService.CreateComment(uId, postId, commentRequest);
            return Ok(response);
        }


        [HttpGet]
        [SwaggerOperation(Summary = ("comments from the commentId"))]
        public async Task<IActionResult> GetCommentById([FromQuery] Guid commentId)
        {
            var comment = await _commentService.GetCommentById(commentId);
            return StatusCode(comment.StatusCode, comment);
        }

        [HttpGet("postcomments")]
        [SwaggerOperation(Summary = ("comments from postID"))]
        public async Task<IActionResult> GetCommentByPostId([FromQuery] Guid postId)
        {
            var comments = await _commentService.GetCommentByPostId(postId);
            return Ok(comments);
        }
        [HttpGet("articlecomments")]
        [SwaggerOperation(Summary = ("comments from ArticleID"))]
        public async Task<IActionResult> GetCommentByArticleId([FromQuery] Guid postId)
        {
            var comments = await _commentService.GetCommentByPostId(postId);
            return Ok(comments);
        }

        [HttpGet("usercomments")]
        [SwaggerOperation(Summary = ("comments from userID"))]
        public async Task<IActionResult> GetCommentByUserId([FromQuery] Guid userId)
        {
            var comments = await _commentService.GetCommentByUserId(userId);
            return Ok(comments);
        }

        [HttpPatch("usercomments")]
        [SwaggerOperation(Summary = ("update comments"))]
        public async Task<IActionResult> UpdateComment(CommentUpdateRequest commentUpdateRequest)
        {
            var comments = await _commentService.EditComment( commentUpdateRequest);
            return Ok(comments);
        }

        [HttpDelete]
        [SwaggerOperation(Summary =("delete comment"))]
        public IActionResult DeleteById([FromQuery] Guid id)
        {
            var result = _commentService.DeleteComment(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
