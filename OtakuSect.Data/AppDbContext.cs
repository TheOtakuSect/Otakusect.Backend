using Microsoft.EntityFrameworkCore;

namespace OtakuSect.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Attachment> Attachments{ get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<UserArticle> UsersArticles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.User);
                entity.HasOne(d => d.Post);
                entity.HasOne(d => d.Article);
                entity.HasMany(d => d.Attachments);
            });
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasOne(d => d.User);
                entity.HasMany(d => d.Comments).WithOne(x=>x.Post).OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(d => d.Attachments);
            });
            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasOne(d => d.Article);
                entity.HasOne(d => d.Post);
                entity.HasOne(d => d.Comment);

            });
            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasOne(d => d.Article);
                entity.HasOne(d => d.Post);
                entity.HasOne(d => d.Comment);
                
            });
            modelBuilder.Entity<UserArticle>(entity =>
            {
                entity.HasOne(d => d.User);
                entity.HasOne(d => d.Article);
            });
        }
    }
}
