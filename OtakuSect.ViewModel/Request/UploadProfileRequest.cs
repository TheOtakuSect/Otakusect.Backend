using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.ViewModel.Request
{
    public class UploadProfileRequest
    {
        public IFormFile formFile { get; set; }
    }
}
