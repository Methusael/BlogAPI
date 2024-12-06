using BlogBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogBackend.Infrastructure.Configuration
{
    internal class CommentBuilder
    {
        public static void InjectConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)               // A comment belongs to one post
                .WithMany(p => p.Comments)         // A post has many comments
                .HasForeignKey(c => c.PostId)      // Foreign Key in Comment
                .OnDelete(DeleteBehavior.Cascade); // Delete comment when post is deleted

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)             // A comment has one Author
                .WithMany(u => u.Comments)         // A user has many comments
                .HasForeignKey(c => c.AuthorId)    // Foreign Key in Comment
                .OnDelete(DeleteBehavior.Cascade); // Delete comment when author is deleted
        }
    }
}
