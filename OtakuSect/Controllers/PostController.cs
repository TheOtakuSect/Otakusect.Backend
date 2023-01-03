using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.BussinessLayer;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel;
using System.Security.Claims;

namespace OtakuSect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public IAuthService _authService;
        public IAttachmentService _attachmentService;
        public IPostService _postService;
        public IPostRepository _postRepository;

        public PostController(IPostRepository postRepository,IAttachmentService attachmentService,IPostService postService, IAuthService authService)
        {
            _postRepository = postRepository;
            _attachmentService = attachmentService;
            _postService = postService;
            _authService = authService;
        }
        [HttpPost("postcontent")]
        public IActionResult PostContent([FromForm]PostViewModel postViewModel)
        {
            var uId = _authService.GetCurrentUser(HttpContext.User.Identity as ClaimsIdentity).UserId;
            var post = _postService.PostContent(uId,postViewModel);
            return Ok(post);
        }
    }
}
