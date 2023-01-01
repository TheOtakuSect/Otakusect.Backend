using Microsoft.AspNetCore.Mvc;
using OtakuSect.Data;
using OtakuSect.Data.GenericRepositories;
using OtakuSect.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<User> UpdateUser(Guid id)
        {
            var result = await _userRepository.UpdateAsync(id);
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
