using BlogBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogBackend.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // DbSet properties representing tables in the database
        public DbSet<User> Users { get; set; }

        public DbSet<Topic> Posts { get; set; }

        public DbSet<PostHistory> PostHistories { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<TopicHistory> TopicHistories{ get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}