using OtakuSect.ViewModel;
using System.Security.Claims;

namespace OtakuSect.BussinessLayer
{
    public interface IAuthService
    {
        public UserClaimModel GetCurrentUser(ClaimsIdentity identity);
        public Task<RegisterUserTokenViewModel> Register(UserViewModel user);
        public Task<string?> Login(string userName, string password);
    }
}
