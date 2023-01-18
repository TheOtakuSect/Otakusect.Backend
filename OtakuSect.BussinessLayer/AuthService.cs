using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using OtakuSect.BussinessLayer.Helper;
using OtakuSect.Data;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OtakuSect.BussinessLayer
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepo;

        public AuthService(IConfiguration config, IUserRepository userRepo)
        {
            _config = config;
            _userRepo = userRepo;
        }

        /// <summary>
        /// Getting Current User from Claim Identity 
        /// </summary>
        public UserClaimModel GetCurrentUser(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserClaimModel
                {
                    UserId = Guid.Parse(userClaims.First(o => o.Type == ClaimTypes.NameIdentifier).Value),
                    EmailAddress = userClaims.First(o => o.Type == ClaimTypes.Email).Value,
                    Role = userClaims.First(o => o.Type == ClaimTypes.Role).Value,
                };
            }
            return new UserClaimModel();
        }

        /// <summary>
        /// Login using username and password
        /// </summary>
        public async Task<string?> Login(string userName, string password)
        {
            var user = await Authenticate(userName, password);
            if (user != null)
            {
                var token = GenerateToken(user);
                return token;
            }
            return null;
        }

        /// <summary>
        /// Register new user using user:UserViewModel
        /// </summary>
        public async Task<RegisterUserTokenViewModel> Register(UserViewModel user)
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                UserRoleId = Guid.Parse("f5f8eda2-be15-48dc-b5e5-51008897fc34"),
                UserName= user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                Password = PasswordHasher.Password2hash(user.Password),
            };
            await _userRepo.AddAsync(newUser);
            var token = await Login(newUser.UserName, newUser.Password);
            var registerToken = new RegisterUserTokenViewModel()
            {
                UserToken =token
            };
            return registerToken;
        }

        #region Private Methods
        /// <summary>
        /// Generates JWT token for the authentication
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateToken(User user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {  
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.EmailAddress),
                new Claim(ClaimTypes.Role,user.UserRole.Role),
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(2880),
                signingCredentials: credentials);
            var encodedJWT = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedJWT;
        }
        /// <summary>
        /// Authenticates User
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<User> Authenticate(string userName, string password)
        {
            var user = await _userRepo.GetUserNameandPassword(userName, password);
            if (user != null)
            {
                return user;
            }
            return new User();
        }
        #endregion
    }
}
