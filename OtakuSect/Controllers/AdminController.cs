using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.BussinessLayer;

namespace OtakuSect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("changerole/{uId}")]
        [Authorize(Roles ="SectMaster")]
        public IActionResult ChangeUserRole(Guid uId)
        {
            var result = _adminService.ChangeRole(uId);
            return Ok(result);
        }
        [HttpPost("getalluser")]
        [Authorize(Roles = "SectMaster")]
        public IActionResult GetallUser()
        {
            var result = _adminService.GetAllUser();
            return Ok(result);
        }
    }
}
