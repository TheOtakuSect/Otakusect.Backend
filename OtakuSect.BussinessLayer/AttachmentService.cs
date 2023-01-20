using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OtakuSect.Data;

namespace OtakuSect.BussinessLayer
{
    public class AttachmentService : IAttachmentService
    {
        private IWebHostEnvironment _webHostEnvironment;
        public AttachmentService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public List<Attachment> UploadFile(List<IFormFile> files)
        {
            var list_attachment = new List<Attachment>();
            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
            foreach (var file in files)
            {
                var newGuid = Guid.NewGuid();
                var attachement = new Attachment()
                {
                    Id = newGuid,
                    Name = newGuid.ToString() + file.FileName
                };
                string filepath = Path.Combine(directoryPath, attachement.Name);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                list_attachment.Add(attachement);
            }
            return list_attachment;
        }

        public Attachment UploadProfile(IFormFile file)
        {
            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/profile");
            var newGuid = Guid.NewGuid() ;
            var profile = new Attachment()
            {
                Id = newGuid,
                Name = newGuid.ToString() + file.FileName
            };
            string filepath = Path.Combine(directoryPath, profile.Name);
            using var stream = new FileStream(filepath, FileMode.Create);
            file.CopyTo(stream);
            return profile;
        }
    }
}
