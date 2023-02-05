using OtakuSect.BussinessLayer.Helper;
using OtakuSect.BussinessLayer.Services.Interface;
using OtakuSect.BussinessLayer.Transformers;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepo;

        public AdminService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<ApiResponse<bool>> ChangeRole(Guid id)
        {
            try
            {
                var user = await _userRepo.GetByIdAsync(id);
                user.UserRoleId = Guid.Parse("b2d85906-2bb2-41e7-8d5e-85800a4d5f4e");
                _userRepo.UpdateAsync(user);
                return ResponseCreater<bool>.CreateSuccessResponse(true, "User role changed successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<bool>.CreateErrorResponse(false, ex.ToString());
            }
        }

        public async Task<ApiResponse<List<UserResponse>>> GetAllUser()
        {
            try
            {
                var users = await _userRepo.GetAllAsync(x => x.UserRole);
                var response = UserTransformer.GetUserResponseFromUser(users.ToList());
                return ResponseCreater<List<UserResponse>>.CreateSuccessResponse(response, "Users loaded succesfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<List<UserResponse>>.CreateErrorResponse(null, ex.ToString());
            }
        }
    }
}
