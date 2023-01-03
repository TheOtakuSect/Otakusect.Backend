using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing.Constraints;
using OtakuSect.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.BussinessLayer
{
    public class AttachmentService : IAttachmentService
    {
        private IWebHostEnvironment _webHostEnvironment;
        public AttachmentService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public List<Data.Attachment> UploadFile(List<IFormFile> files)
        {
           var list_attachment = new List<Data.Attachment>();   
                string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
                foreach (var file in files)
                {
                    var newGuid = Guid.NewGuid();
                    var attachement = new Data.Attachment()
                    {
                        Id = newGuid,
                        Name = newGuid.ToString() + file.FileName
                    };
                    string filepath = Path.Combine(directoryPath,attachement.Name);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    list_attachment.Add(attachement);
                }
                return list_attachment;
        }
    }
}
