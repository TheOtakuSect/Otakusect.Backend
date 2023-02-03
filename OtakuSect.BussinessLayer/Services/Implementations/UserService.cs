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

        public async Task<ApiResponse<UserResponse>> UpdateUser(UserUpdateRequest userUpdateRequest)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userUpdateRequest.Id);
                if (user == null)
                {
                    return ResponseCreater<UserResponse>.CreateNotFoundResponse(null, "User not found");
                }
                user.UserName = userUpdateRequest.UserName;
                user.FullName = userUpdateRequest.FullName;
                user.EmailAddress = userUpdateRequest.EmailAddress;
                user.ProfilePic = _attachmentService.UploadProfile(userUpdateRequest.File);
                _userRepository.UpdateAsync(user);

                var response = UserTransformer.GetUserResponseFromUser(user);
                return ResponseCreater<UserResponse>.CreateSuccessResponse(response, "User updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<UserResponse>.CreateErrorResponse(null, ex.ToString());
            }
        }
    }
}
