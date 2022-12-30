using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OtakuSect.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using OtakuSect.Data.DTO;
using OtakuSect.Data.GenericRepositories;
using OtakuSect.ViewModel;
using OtakuSect.Helper;
using OtakuSect.Data.Repositories;
using Newtonsoft.Json;

namespace OtakuSect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _iconfig;
        private readonly IUserRepository userRepo;
        public AuthController(IConfiguration iconfig, IUserRepository userRepository)
        {
            _iconfig = iconfig;
            userRepo = userRepository;
        }
        [HttpGet("Admins")]
        [Authorize(Roles = "SectMaster")]
        public IActionResult AdminsEndPoint()
        {
            var currentUser = GetCurrentUser();
            if (currentUser.Role != "admin")
            {
                return Unauthorized("Only sectmaster is allowed here");
            }
            return Ok($" Hi {currentUser.UserId} youre  an imortal being {currentUser.Role}");
        }
        private UserClaimModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserClaimModel
                {
                    UserId = Guid.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value),
                    EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                };
            }
            return new UserClaimModel();
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult RegisterUser(UserViewModel user)
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                UserRoleId = Guid.Parse("f5f8eda2-be15-48dc-b5e5-51008897fc34"),
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                Password = PasswordHasher.Password2hash(user.Password),
            };
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            var user = await Authenticate(userDTO);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }
        private async Task<string> Generate(User user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfig["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.EmailAddress),
                new Claim(ClaimTypes.Role,user.UserRole.Role),
            };

            var token = new JwtSecurityToken(_iconfig["Jwt:Issuer"],
                _iconfig["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            var encodedJWT = new JwtSecurityTokenHandler().WriteToken(token);
            
            return encodedJWT;
        }

        private async Task<User> Authenticate(UserDTO userDTO)
        {

            var current_user = userRepo.GetUserNameandPassword(userDTO.UserName, PasswordHasher.Password2hash(userDTO.Password));
            if (current_user != null)
            {
                return await current_user;
            }
            return null;
        }

    }
}
