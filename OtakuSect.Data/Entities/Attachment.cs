namespace OtakuSect.Data.Entities
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Post Post { get; set; }
        public Guid? PostId { get; set; }
        public Article Article { get; set; }
        public Guid? ArticleId { get; set; }
        public Comment Comment { get; set; }
        public Guid? CommentId { get; set; }
    }
}
