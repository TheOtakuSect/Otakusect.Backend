using Microsoft.AspNetCore.Http;
using OtakuSect.Data;

namespace OtakuSect.BussinessLayer
{
    public interface IProfileService
    {
        public Task<User> UploadProfile(IFormFile file);
    }
}
