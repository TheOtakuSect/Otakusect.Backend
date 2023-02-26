using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.BussinessLayer.Services.Implementations;
using OtakuSect.BussinessLayer.Services.Interface;
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

        [HttpPost("user/role")]
        [SwaggerOperation(Summary = "Change User Role to Sect Elder")]
        public async Task<IActionResult> ChangeUserRole([FromQuery] Guid id)
        {
            var result = await _adminService.ChangeRole(id);
            return Ok(result);
        }

        [HttpGet("users")]
        [SwaggerOperation(Summary = "Get all users only possible by admin")]
        public async Task<IActionResult> GetallUser()
        {
            var result = await _adminService.GetAllUser();
            return Ok(result);
        }
    }
}
