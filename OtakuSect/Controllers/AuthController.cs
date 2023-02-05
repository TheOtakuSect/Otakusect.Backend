using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.BussinessLayer.Services.Interface;
using OtakuSect.ViewModel.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace OtakuSect.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Register user as disciple")]
        public async Task<IActionResult> RegisterUser(UserRegisterRequest user)
        {
            var result = await _authService.Register(user);
            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "User login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest)
        {
            var token = await _authService.Login(loginRequest.UserName, loginRequest.Password);
            if (token == null)
            {
                return NotFound("User not found");
            }
            return Ok(token);
        }
    }
}
