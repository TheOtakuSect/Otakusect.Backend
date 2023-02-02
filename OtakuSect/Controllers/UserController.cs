using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using OtakuSect.BussinessLayer;
using OtakuSect.Data;
using OtakuSect.Data.GenericRepositories;
using OtakuSect.ViewModel.Request;
using Swashbuckle.AspNetCore.Annotations;
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

        #region  Update User
        [HttpPut("update")]
        [Authorize(Roles ="SectMaster,SectElder,Disciple")]
        [SwaggerOperation("Update Users")]
        public async Task<IActionResult> Update([FromForm]UserUpdateViewModel userUpdateViewModel)
        {
            var uId = _authService.GetCurrentUser(HttpContext.User.Identity as ClaimsIdentity).UserId;
            var result = await userService.UpdateUser(uId,userUpdateViewModel);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        #endregion
    }
}