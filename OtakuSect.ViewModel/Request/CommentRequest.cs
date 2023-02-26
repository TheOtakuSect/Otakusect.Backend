using Microsoft.AspNetCore.Http;

namespace OtakuSect.ViewModel.Request
{
    public class CommentRequest
    {
        public string Description { get; set; }
        public List<IFormFile> Files { get; set; }
    }
    public class CommentUpdateRequest : CommentRequest
    {
        public Guid Id { get; set; }
    }

}
