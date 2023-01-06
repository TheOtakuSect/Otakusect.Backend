using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.BussinessLayer;
using OtakuSect.ViewModel;
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

        [SwaggerOperation(Summary = "Save Post in database")]
        [HttpPost]
        public IActionResult PostContent([FromForm] PostViewModel postViewModel)
        {
            var uId = _authService.GetCurrentUser(HttpContext.User.Identity as ClaimsIdentity).UserId;
            var post = _postService.PostContent(uId, postViewModel);
            return Ok(post);
        }

        [SwaggerOperation(Summary = "Get Post by Id")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var result = await _postService.GetPostById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [SwaggerOperation(Summary = "Delete Post by Id")]
        [HttpDelete]
        public IActionResult DeleteById([FromQuery] Guid id)
        {
            var result = _postService.DeletePost(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [SwaggerOperation(Summary="Edit the post")]
        [HttpPut]
        [Authorize(Roles ="SectMaster, SectElder, Disciple")]
        public async Task<IActionResult> EditPost([FromQuery] Guid id,[FromForm]PostViewModel postViewModel)
        {
            var updatedPost = await _postService.EditPost(id, postViewModel);
            return Ok(updatedPost);
        }
    }
}
