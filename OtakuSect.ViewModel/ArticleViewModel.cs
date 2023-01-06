using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.ViewModel
{
    public class ArticleViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }
        public List<IFormFile> Files { get; set; }

    }
}
