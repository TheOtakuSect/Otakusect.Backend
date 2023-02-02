using OtakuSect.Data;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel.Request;

namespace OtakuSect.BussinessLayer
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAttachmentService _attachmentService;
        public UserService(IUserRepository userRepository,IAttachmentService attachmentService)
        {
            _userRepository = userRepository;
            _attachmentService = attachmentService;
        }
       
        /// <summary>
        /// Lets the user to update information
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> UpdateUser(Guid uId, UserUpdateViewModel userUpdateViewModel)
        {
            var user = await _userRepository.GetByIdAsync(uId);
            if (user == null)
            {
                throw new Exception("user not found");
            }
            user.UserName= userUpdateViewModel.UserName;
            user.FullName= userUpdateViewModel.FullName;    
            user.EmailAddress   = userUpdateViewModel.EmailAddress;
            user.ProfilePic = _attachmentService.UploadProfile(userUpdateViewModel.File);
            _userRepository.UpdateAsync(user);
            return user;
        }
    }
}
