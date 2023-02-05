namespace OtakuSect.Data.Entities
{
    public class Rate
    {
        public Guid Id { get; set; }
        public double Rating { get; set; }
        public Post Post { get; set; }
        public Guid PostId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
