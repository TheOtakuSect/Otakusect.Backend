using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using OtakuSect.BussinessLayer;
using OtakuSect.Data;
using OtakuSect.Data.GenericRepositories;
using OtakuSect.ViewModel;
using System.Security.Claims;
namespace OtakuSect.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthService authService;
        public UserController(IUserService userService, IAuthService authService)
        {
            this.userService = userService;
            this.authService = authService;
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var result = userService.UpdateUser(user);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}