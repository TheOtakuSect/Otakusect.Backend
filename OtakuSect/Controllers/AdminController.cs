using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.BussinessLayer;
using Swashbuckle.AspNetCore.Annotations;

namespace OtakuSect.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "SectMaster")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [SwaggerOperation(Summary = "Change User Role to Sect Leader")]
        [HttpPost("user/role")]
        public IActionResult ChangeUserRole([FromQuery]Guid id)
        {
            var result = _adminService.ChangeRole(id);
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Get all Users")]
        [HttpGet("users")]
        public IActionResult GetallUser()
        {
            var result = _adminService.GetAllUser();
            return Ok(result);
        }
    }
}
