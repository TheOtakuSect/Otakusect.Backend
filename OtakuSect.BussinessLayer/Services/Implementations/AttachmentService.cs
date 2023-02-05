using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OtakuSect.BussinessLayer.Services.Interface;
using OtakuSect.Data.Entities;

namespace OtakuSect.BussinessLayer.Services.Implementations
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
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
                    Name = newGuid.ToString() + file.FileName.Split('.')[1]
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

        public string UploadProfile(IFormFile file)
        {
            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/profile");
            var image = Guid.NewGuid().ToString() +file.FileName.Split('.')[1];
            string filepath = Path.Combine(directoryPath, image);
            using var stream = new FileStream(filepath, FileMode.Create);
            file.CopyTo(stream);
            return image;
        }
    }
}
