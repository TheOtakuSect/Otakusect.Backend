using Microsoft.AspNetCore.Http;
using OtakuSect.Data.Entities;

namespace OtakuSect.BussinessLayer.Services.Interface
{
    public interface IAttachmentService
    {
        public List<Attachment> UploadFile(List<IFormFile> files);
        public string UploadProfile(IFormFile file);
    }
}
