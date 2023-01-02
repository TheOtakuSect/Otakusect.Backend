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
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.BussinessLayer
{
   public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> UpdateUser(User user)
        {
            var result = await _userRepository.UpdateAsync(user);
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
