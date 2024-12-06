using BlogBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogBackend.Infrastructure.Configuration
{
    internal class TopicBuilder
    {
        public static void InjectConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>()
                .HasOne(t => t.Author)                 // A topic has one Author
                .WithMany(u => u.Topics)               // A user has many topics
                .HasForeignKey(t => t.AuthorId)        // Foreign Key in Topic
                .OnDelete(DeleteBehavior.NoAction);    // No action when Author deleted
        }
    }
}
