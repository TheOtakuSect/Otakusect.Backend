using Microsoft.AspNetCore.Authorization;
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
        private readonly CheckUserService _checkUserService;

        public AdminController(IAdminService adminService, CheckUserService checkUserService)
        {
            _adminService = adminService;
            _checkUserService = checkUserService;
        }

        [SwaggerOperation(Summary = "Change User Role to Sect Elder")]
        [HttpPost("user/role")]
        public IActionResult ChangeUserRole([FromQuery] Guid id)
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

        [SwaggerOperation(Summary = "Check if user exists")]
        [HttpPost("checkuser")]
        public bool CheckUser(string userName)
        {
            return _checkUserService.CheckUser(userName);
        }
    }
}
