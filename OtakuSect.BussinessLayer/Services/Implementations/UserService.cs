using OtakuSect.BussinessLayer.Helper;
using OtakuSect.BussinessLayer.Services.Interface;
using OtakuSect.BussinessLayer.Transformers;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel.Request;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAttachmentService _attachmentService;
        public UserService(IUserRepository userRepository, IAttachmentService attachmentService)
        {
            _userRepository = userRepository;
            _attachmentService = attachmentService;
        }

        public async Task<ApiResponse<List<UserResponse>>> AllUsers()
        {
            {
                try
                {
                    var results = await _userRepository.GetAllAsync(u => u.UserRole);
                    var response = UserTransformer.GetUserResponseFromUser(results.ToList());
                    return ResponseCreater<List<UserResponse>>.CreateSuccessResponse(response, "Users loaded successfully");
                }
                catch (Exception ex)
                {
                    return ResponseCreater<List<UserResponse>>.CreateErrorResponse(null, ex.ToString());
                }
            }
        }

        public ApiResponse<bool> CheckUser(string userName)
        {
            try
            {
                var userExists = _userRepository.CheckUserName(userName);
                return ResponseCreater<bool>.CreateSuccessResponse(userExists, userExists ? "User exists" : "User don't exists");
            }
            catch (Exception ex)
            {
                return ResponseCreater<bool>.CreateErrorResponse(false, ex.ToString());
            }
        }

        public async Task<ApiResponse<UserResponse>> EditUser(Guid uId, UserUpdateRequest request)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(uId);
                user.FullName = request.FullName;
                user.UserName = request.UserName;
                user.EmailAddress = request.EmailAddress;
                _userRepository.UpdateAsync(user);
                var response = UserTransformer.GetUserResponseFromUser(user);
                return ResponseCreater<UserResponse>.CreateSuccessResponse(response, "user updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<UserResponse>.CreateErrorResponse(null, ex.ToString());

            }

        }

        public async Task<ApiResponse<List<UserResponse>>> GetElders()
        {
            try
            {
                var elders = await _userRepository.GetAllAsync(u => u.UserRole);
                var results = elders.Where(x => x.UserRole.Role == "SectElder").ToList();
                var response = UserTransformer.GetUserResponseFromUser(results);
                return ResponseCreater<List<UserResponse>>.CreateSuccessResponse(response, "Users loaded successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<List<UserResponse>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<ApiResponse<UserResponse>> GetUser(Guid Id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(Id);
                var response = UserTransformer.GetUserResponseFromUser(user);
                return ResponseCreater<UserResponse>.CreateSuccessResponse(response, "Users loaded successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<UserResponse>.CreateErrorResponse(null, ex.ToString());

            }
        }
    }
}
