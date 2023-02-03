using OtakuSect.Data.Entities;
using OtakuSect.ViewModel.Response;

namespace OtakuSect.BussinessLayer.Services.Interface
{
    public interface IAdminService
    {
        public Task<ApiResponse<bool>> ChangeRole(Guid uId);
        public Task<ApiResponse<List<UserResponse>>> GetAllUser();
    }
}
