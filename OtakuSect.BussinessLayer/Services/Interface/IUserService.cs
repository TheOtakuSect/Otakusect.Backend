using Microsoft.AspNetCore.JsonPatch;
using OtakuSect.ViewModel.Request;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer.Services.Interface
{
    public interface IUserService
    {
        public ApiResponse<bool> CheckUser(string userName);
        public Task<ApiResponse<List<UserResponse>>> GetElders();
        public Task<ApiResponse<List<UserResponse>>> AllUsers();

        public Task<ApiResponse<UserResponse>> GetUser(Guid Id);
        public Task<ApiResponse<UserResponse>> EditUser( Guid uId, UserUpdateRequest request);

    }
}
