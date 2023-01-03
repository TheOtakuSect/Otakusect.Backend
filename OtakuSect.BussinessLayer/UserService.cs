using OtakuSect.Data;
using OtakuSect.Data.Repositories;
using OtakuSect.ViewModel;

namespace OtakuSect.BussinessLayer
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public UserService(IUserRepository userRepository, IAuthService authServce, AppDbContext context)
        {
            _userRepository = userRepository;
            _authService = authServce;
        }

        public async IAsyncEnumerable<User> GetAllUser()
        {
            var users = await _userRepository.GetAllAsync();
            foreach (var user in users)
            {
                yield return user;
            }
        }


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
