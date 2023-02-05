namespace OtakuSect.Data.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid ParentCommentId { get; set; }
        public string Description { get; set; }
        public int Upvote { get; set; }
        public int Downvote { get; set; }
        public DateTime CommentDateTime { get; set; } = DateTime.Now;
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Post Post { get; set; }
        public Guid PostId { get; set; }
        public Article Article { get; set; }
        public Guid ArticleId { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
