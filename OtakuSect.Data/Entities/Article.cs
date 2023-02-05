namespace OtakuSect.Data.Entities
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }
        public List<Category> Categories { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<Comment> Comments { get; set; }
        public List<UserArticle> UserArticles { get; set; }
    }
}
