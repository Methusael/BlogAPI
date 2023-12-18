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
        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }
        // Add other DbSet properties for additional entities
    }
}
