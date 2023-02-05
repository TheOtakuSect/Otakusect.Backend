namespace OtakuSect.ViewModel.Response
{
    public class ArticleResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Attachments { get; set; }
        public List<ArticleUser> Users { get; set; }
    }

    public class ArticleUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
