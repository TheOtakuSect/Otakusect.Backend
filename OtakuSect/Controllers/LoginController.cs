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
namespace OtakuSect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _iconfig;
        private AppDbContext _context;

        public LoginController(IConfiguration iconfig, AppDbContext context)
        {
            _iconfig = iconfig;
            _context = context;

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserDTO userDTO)
        {
            var user = Authenticate(userDTO);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }
        private string Generate(UserModel user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfig["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Email,user.EmailAddress),
                new Claim(ClaimTypes.GivenName,user.FirstName),

                new Claim(ClaimTypes.Surname,user.LastName),
                new Claim(ClaimTypes.Role,user.Role),
            };

            var token = new JwtSecurityToken(_iconfig["Jwt:Issuer"],
                _iconfig["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

            private UserModel Authenticate(UserDTO userDTO)
        {
            
            var current_user =  _context.users.FirstOrDefault(o=>o.UserName.ToLower()==userDTO.UserName.ToLower()&& o.Password == password2hash(userDTO.Password));
            if (current_user != null)
            {
                return current_user;
            }
            return null;
        }

        private string password2hash(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            byte[] hash = hashAlgorithm.ComputeHash(passwordBytes);
            string hashString = BitConverter.ToString(hash).Replace("-", string.Empty);
            return hashString;
        }
    }
}
