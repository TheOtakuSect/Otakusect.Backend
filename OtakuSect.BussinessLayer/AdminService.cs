using Microsoft.AspNetCore.Http;
using OtakuSect.Data;
using OtakuSect.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.BussinessLayer
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        public AdminService(IUserRepository userRepository)
        {
            _userRepository= userRepository;
        }
        /// <summary>
        /// change the user role from disciple to SectElder
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        public async Task<ApiResponse<User>> ChangeRole(Guid uId)
        {
            var user = await _userRepository.GetByIdAsync(uId);
            var apiResponse = new ApiResponse<User>();
            try
            {
                user.UserRoleId = Guid.Parse("b2d85906-2bb2-41e7-8d5e-85800a4d5f4e");
                _userRepository.UpdateAsync(user);
                apiResponse.Success = true;
                apiResponse.Message = "user role changed";
                apiResponse.Data = user;
                return apiResponse;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode= 500;
                apiResponse.Data = null;
                return apiResponse;
            }
        }
        /// <summary>
        /// Returns all the users
        /// </summary>
        /// <returns></returns>
        public async IAsyncEnumerable<User> GetAllUser()
        {
            var users = await _userRepository.GetAllAsync("UserRole");
           
            foreach (var user in users)
            {
                yield return user;
            }
        }
    }
}
