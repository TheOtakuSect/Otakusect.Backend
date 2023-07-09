using OtakuSect.Data.Entities;

namespace OtakuSect.ViewModel.Response
{
    public class CommentResponse
    {
        public Guid Id { get; set; }
        public Guid ParentCommentId { get; set; }
        public string Description { get; set; }
        public int Upvote { get; set; }
        public int Downvote { get; set; }
        public CommentUser User { get; set; }
        public List<string> Attachments { get; set; }
    }
    public class CommentUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

    }
}