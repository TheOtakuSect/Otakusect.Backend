using OtakuSect.ViewModel;
using OtakuSect.ViewModel.Request;
using OtakuSect.ViewModel.Response;
using System.Security.Claims;

namespace OtakuSect.BussinessLayer
{
    public interface IAuthService
    {
        public UserClaimModel GetCurrentUser(ClaimsIdentity identity);
        public Task<ApiResponse<RegisterUserTokenViewModel>> Register(UserViewModel user);
        public Task<string?> Login(string userName, string password);
    }
}
