using Microsoft.AspNetCore.Http;

namespace OtakuSect.ViewModel.Request
{
    public class ArticleRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<IFormFile> Files { get; set; }
    }

    public class ArticleUpdateRequest : ArticleRequest
    {
        public Guid Id { get; set; }
    }
}
