namespace OtakuSect.ViewModel.Response
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsSafeToWatch { get; set; }
        public string Tags { get; set; }
        public DateTime PostedDateTime { get; set; }
        public double TotalRate { get; set; }
        public int ViewCount { get; set; }
        public PostUser User { get; set; }
        public List<string> Attachments { get; set; }
    }

    public class PostUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
