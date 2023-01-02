using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OtakuSect.Controllers
{
    [Route("api/attachment")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private IWebHostEnvironment _webHostEnvironment;
        public AttachmentController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("uploadfiles")]
        public IActionResult UploadFiles(List<IFormFile> files)
        {
            if (files.Count == 0)
            {
                return BadRequest();
            }
            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
            foreach (var file in files)
            {
                string filepath = Path.Combine(directoryPath, file.FileName);
                using (var stream = new FileStream(filepath,FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return Ok("uploaded Files");
        }
    }
}
