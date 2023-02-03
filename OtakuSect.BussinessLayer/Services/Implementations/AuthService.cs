using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OtakuSect.BussinessLayer.Helper;
using OtakuSect.BussinessLayer.Services.Interface;
using OtakuSect.Data;
using OtakuSect.Data.Entities;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel;
using OtakuSect.ViewModel.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OtakuSect.BussinessLayer.Services.Implementations
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

        public async Task<ApiResponse<string>> Login(string userName, string password)
        {
            try
            {
                var user = await Authenticate(userName, password);
                if (user != null)
                {
                    var token = GenerateToken(user);
                    return ResponseCreater<string>.CreateSuccessResponse(token, "User login found.");
                }
                else
                {
                    return ResponseCreater<string>.CreateNotFoundResponse(null, "User not found.");
                }
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<ApiResponse<string>> Register(UserRegisterRequest user)
        {
            try
            {
                var usernameExists = _userRepo.CheckUserName(user.UserName);
                var emailExists = _userRepo.CheckEmail(user.EmailAddress);
                if (!usernameExists && !emailExists)
                {
                    var newUser = new User()
                    {
                        Id = Guid.NewGuid(),
                        UserRoleId = Guid.Parse(Constants.DiscipleRoleId),
                        UserName = user.UserName,
                        FullName = user.FullName,
                        EmailAddress = user.EmailAddress,
                        Password = PasswordHasher.Password2hash(user.Password),
                    };
                    await _userRepo.AddAsync(newUser);
                    newUser.UserRole = new UserRole()
                    {
                        Id = Guid.Parse(Constants.DiscipleRoleId),
                        Role = Constants.DiscipleRoleName
                    };
                    var token = GenerateToken(newUser);
                    return ResponseCreater<string>.CreateSuccessResponse(token, "User registered successfully");
                }
                else
                {
                    var duplicateKeys = @$"username:{usernameExists} email:{emailExists}";
                    return ResponseCreater<string>.CreateDuplicateExistResponse(duplicateKeys, "Following key already exists");
                }
            }
            catch (Exception ex)
            {
                return ResponseCreater<string>.CreateErrorResponse(null, ex.ToString());
            }
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
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials);
            var encodedJWT = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedJWT;
        }
        /// <summary>
        /// Authenticates User
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>Get User from DB</returns>
        private async Task<User> Authenticate(string userName, string password)
        {
            var user = await _userRepo.GetUser(userName, PasswordHasher.Password2hash(password));
            if (user != null)
            {
                return user;
            }
            return new User();
        }
        #endregion
    }
}

