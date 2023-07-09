using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.BussinessLayer.Services.Interface;
using OtakuSect.ViewModel.Request;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
namespace OtakuSect.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet("checkuser")]
        [SwaggerOperation(Summary = "Check if user exists")]
        public IActionResult CheckUser([FromQuery] string userName)
        {
            return Ok(_userService.CheckUser(userName));
        }

        [HttpPut("update")]
        [Authorize]
        [SwaggerOperation(Summary = "update the user")]
        public IActionResult UpdateUser([FromForm] UserUpdateRequest request)
        {
            var uId = _authService.GetCurrentUser(HttpContext.User.Identity as ClaimsIdentity).UserId;
            var result = _userService.EditUser(uId, request);
            return Ok(result);
        }

        [HttpGet("user")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "gets user by id")]
        public async Task<IActionResult> GetUserById([FromQuery] Guid userId)
        {
            var user = await _userService.GetUser(userId);
            return Ok(user);
        }

        [HttpGet("allusers")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "gets all users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userService.AllUsers();
            return Ok(user);
        }


        [HttpGet("getelders")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "get the sect elders")]
        public async Task<IActionResult> GetSectElders()
        {
            var result = await _userService.GetElders();
            return Ok(result);
        }
    }
}