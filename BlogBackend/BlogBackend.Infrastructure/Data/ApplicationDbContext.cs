using BlogBackend.Domain.Models;
using BlogBackend.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BlogBackend.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<History<Topic>> TopicHistories { get; set; }
        public DbSet<History<Post>> PostHistories { get; set; }
        public DbSet<History<Comment>> CommentHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Injecting configurations
            TopicBuilder.InjectConfiguration(modelBuilder);
            PostBuilder.InjectConfiguration(modelBuilder);
            CommentBuilder.InjectConfiguration(modelBuilder);
            HistoryBuilder.InjectConfiguration(modelBuilder);           
        }
    }
}