using OtakuSect.ViewModel;
using OtakuSect.ViewModel.Request;
using OtakuSect.ViewModel.Response;
using System.Security.Claims;

namespace OtakuSect.BussinessLayer.Services.Interface
{
    public interface IAuthService
    {
        public UserClaimModel GetCurrentUser(ClaimsIdentity identity);
        public Task<ApiResponse<string>> Register(UserRegisterRequest user);
        public Task<ApiResponse<string>> Login(string userName, string password);
    }
}
