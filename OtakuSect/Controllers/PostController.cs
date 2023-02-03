using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.BussinessLayer.Services.Interface;
using OtakuSect.ViewModel.Request;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace OtakuSect.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public IAuthService _authService;
        public IPostService _postService;
        public PostController(IPostService postService, IAuthService authService)
        {
            _postService = postService;
            _authService = authService;
        }

        [HttpGet("all")]
        [SwaggerOperation(Summary = "Get all post, visible to all")]
        public IActionResult GetAllPosts()
        {
            var allPosts = _postService.GetAllPosts();
            return Ok(allPosts);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get post by Id")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var result = await _postService.GetPostById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "Save post in database")]
        public IActionResult PostContent([FromForm] PostRequest PostRequest)
        {
            var uId = _authService.GetCurrentUser(HttpContext.User.Identity as ClaimsIdentity).UserId;
            var post = _postService.PostContent(uId, PostRequest);
            return Ok(post);
        }

        [HttpPut]
        [Authorize]
        [SwaggerOperation(Summary = "Edit post")]
        public async Task<IActionResult> EditPost([FromForm] PostUpdateRequest PostRequest)
        {
            var updatedPost = await _postService.EditPost(PostRequest);
            return Ok(updatedPost);
        }

        [HttpDelete]
        [Authorize]
        [SwaggerOperation(Summary = "Delete post by Id")]
        public IActionResult DeleteById([FromQuery] Guid id)
        {
            var result = _postService.DeletePost(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
