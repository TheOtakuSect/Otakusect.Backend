using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtakuSect.Data;
using OtakuSect.Data.GenericRepositories;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.BussinessLayer
{
   public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public UserService(IUserRepository userRepository, IAuthService authServce,AppDbContext context)
        {
            _userRepository = userRepository;
            _authService = authServce;
        }
        public async Task<User> UpdateUser(Guid uId,UserViewModel user)
        {
            var result =await  _userRepository.GetByIdAsync(uId);
            result.EmailAddress= user.EmailAddress;
            result.FirstName= user.FirstName;
            result.LastName= user.LastName;
            result.UserName= user.UserName;
            await _userRepository.UpdateAsync(result);
            return result;
        }
    }
}
