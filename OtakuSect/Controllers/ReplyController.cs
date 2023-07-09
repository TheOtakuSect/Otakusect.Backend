using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.BussinessLayer.Services.Interface;
using OtakuSect.ViewModel.Request;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace OtakuSect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IAuthService _authService;

        public ReplyController( ICommentService commentService,IAuthService authService)
        {
            _authService = authService;
            _commentService = commentService;
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = ("create reply"))]
        public async Task<IActionResult> CreateReply([FromQuery] Guid parentcommentId, [FromForm] CommentRequest commentRequest)
        {
            var uId = _authService.GetCurrentUser(HttpContext.User.Identity as ClaimsIdentity).UserId;

            var response = await _commentService.CreateReply(uId, parentcommentId, commentRequest);
            return Ok(response);
        }
        [HttpGet]
        [SwaggerOperation(Summary = ("get reply"))]
        public async Task<IActionResult> GetReply([FromQuery] Guid commentId)
        {
            var response = await _commentService.GetReplys(commentId);
            return Ok(response);
        }

        [HttpGet("allreplies")]
        [SwaggerOperation(Summary = ("get reply"))]
        public async Task<IActionResult> GetReplyFromParent([FromQuery] Guid Id)
        {
            var response = await _commentService.GetAllReplies(Id);
            return Ok(response);
        }


    }
}
