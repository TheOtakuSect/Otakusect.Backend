using Microsoft.AspNetCore.Http;

namespace OtakuSect.ViewModel.Request
{
    public class PostRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsSafeToWatch { get; set; }
        public string Tags { get; set; }
        public List<IFormFile> Files { get; set; }
    }
    public class PostUpdateRequest : PostRequest
    {
        public Guid Id { get; set; }
    }
}
