namespace OtakuSect.Data.Entities
{
    public class UserArticle
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Article Article { get; set; }
        public Guid ArticleId { get; set; }
    }
}
