using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.BussinessLayer
{
    public interface IAttachmentService
    {
        public List<Data.Attachment> UploadFile(List<IFormFile> files);
    }
}
