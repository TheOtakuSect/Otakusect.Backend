using Microsoft.AspNetCore.Http;
using OtakuSect.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.ViewModel
{
    public class PostViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsSafeToWatch { get; set; }
        public string Tags { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
