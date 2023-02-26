using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.BussinessLayer.Services.Interface;
using Swashbuckle.AspNetCore.Annotations;

namespace OtakuSect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        [HttpGet("all")]
        [SwaggerOperation(Summary = "Get all post, visible to all")]
        public IActionResult GetAllComments()
        {
            var comments = _commentService.GetComments();
        }

    }
}
   