﻿namespace OtakuSect.Data.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsSafeToWatch { get; set; }
        public string Tags { get; set; }
        public DateTime PostedDateTime { get; set; } = DateTime.Now;
        public double TotalRate { get; set; }
        public int ViewCount { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
