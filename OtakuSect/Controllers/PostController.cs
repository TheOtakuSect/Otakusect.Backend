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
        public IPostService _postService;
        public PostController(IPostService postService, IAuthService authService)
        {
            _postService = postService;
            _authService = authService;
        }
        /// <summary>
        /// Allows user to post the content throught the postViewModel
        /// </summary>
        /// <param name="postViewModel"></param>
        /// <returns></returns>
        [HttpPost("postcontent")]
        public IActionResult PostContent([FromForm]PostViewModel postViewModel)
        {
            var uId = _authService.GetCurrentUser(HttpContext.User.Identity as ClaimsIdentity).UserId;
            var post = _postService.PostContent(uId,postViewModel);
            return Ok(post);
        }

        /// <summary>
        /// Gets Post by throught PostId
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        [HttpGet("getpostbyid/{pId}")]
        public async Task<IActionResult> GetById(Guid pId)
        {
            var result =await  _postService.GetPostById(pId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        /// <summary>
        /// Gets Post by throught PostId
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        [HttpGet("delete/{pId}")]
        public IActionResult DeleteById(Guid pId)
        {
            var result = _postService.DeletePost(pId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
