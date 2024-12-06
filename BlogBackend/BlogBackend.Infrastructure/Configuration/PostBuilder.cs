using BlogBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogBackend.Infrastructure.Configuration
{
    internal class PostBuilder
    {
        public static void InjectConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)              // A post belongs to one Author
                .WithMany(u => u.Posts)             // A user has many posts
                .HasForeignKey(p => p.AuthorId)     // Foreign Key in Post
                .OnDelete(DeleteBehavior.NoAction); // No action when Author deleted

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Topic)               // A post belongs to one Topic
                .WithMany(t => t.Posts)             // A topic has many posts
                .HasForeignKey(p => p.TopicId)      // Foreign Key in Post
                .OnDelete(DeleteBehavior.Cascade);  // Delete post when topic is deleted
        }
    }
}
