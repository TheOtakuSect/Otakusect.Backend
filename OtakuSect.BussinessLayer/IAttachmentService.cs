using Microsoft.AspNetCore.Http;
using OtakuSect.Data;

namespace OtakuSect.BussinessLayer
{
    public interface IAttachmentService
    {
        public List<Attachment> UploadFile(List<IFormFile> files);
        public Attachment UploadProfile(IFormFile file);

    }
}
