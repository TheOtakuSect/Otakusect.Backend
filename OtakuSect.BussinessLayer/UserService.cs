using OtakuSect.Data;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel;

namespace OtakuSect.BussinessLayer
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
       
        /// <summary>
        /// Lets the user to update information
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> UpdateUser(Guid uId, UserUpdateViewModel userUpdateViewModel)
        {
            var result = await _userRepository.GetByIdAsync(uId);
            result.EmailAddress = userUpdateViewModel.EmailAddress;
            result.FullName = userUpdateViewModel.FullName;
            result.UserName = userUpdateViewModel.UserName;
            await _userRepository.UpdateAsync(result);
            return result;
        }
    }
}
