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
        public async Task<User> UpdateUser(Guid uId, UserViewModel user)
        {
            var result = await _userRepository.GetByIdAsync(uId);
            result.EmailAddress = user.EmailAddress;
            result.FirstName = user.FirstName;
            result.LastName = user.LastName;
            result.UserName = user.UserName;
            await _userRepository.UpdateAsync(result);
            return result;
        }
    }
}
