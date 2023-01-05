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
        private readonly IAuthService _authService;
        public UserController(IUserService userService, IAuthService authService)
        {
            this.userService = userService;
            _authService = authService;
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(UserViewModel userViewModel)
        {
            var uId = _authService.GetCurrentUser(HttpContext.User.Identity as ClaimsIdentity).UserId;
            var result = userService.UpdateUser(uId,userViewModel);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}