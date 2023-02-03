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

        [HttpPost("checkuser")]
        [SwaggerOperation(Summary = "Check if user exists")]
        public IActionResult CheckUser(string userName)
        {
            return Ok(_userService.CheckUser(userName));
        }

        [HttpPut("update")]
        [Authorize(Roles = "SectMaster,SectElder,Disciple")]
        [SwaggerOperation(Summary = "Update Users")]
        public async Task<IActionResult> Update([FromForm] UserUpdateRequest userUpdateRequest)
        {
            userUpdateRequest.Id = _authService.GetCurrentUser(HttpContext.User.Identity as ClaimsIdentity).UserId;
            var result = await _userService.UpdateUser(userUpdateRequest);
            return Ok(result);
        }
    }
}