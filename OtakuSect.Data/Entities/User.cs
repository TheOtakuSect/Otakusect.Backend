namespace OtakuSect.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public UserRole UserRole { get; set; }
        public Guid UserRoleId { get; set; }
        public List<UserArticle> UserArticles { get; set; }
        public string ProfilePic { get; set; }
    }
}
