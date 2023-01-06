using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.BussinessLayer;
using OtakuSect.Data.DTO;
using OtakuSect.ViewModel;
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
        [SwaggerOperation("register User")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(UserViewModel user)
        {
            var result =await _authService.Register(user);
            return Ok(result);
        }

        [HttpPost("login")]
        [SwaggerOperation("User login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            var token = await _authService.Login(userDTO.UserName, userDTO.Password);
            if (token == null)
            {
                return NotFound("User not found");
            }
            return Ok(token);
        }

    }
}
