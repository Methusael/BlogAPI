using BlogBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogBackend.Infrastructure.Configuration
{
    internal class HistoryBuilder
    {
        public static void InjectConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<History<Post>>()
                .HasOne(h => h.Entity)                 // History has one Post
                .WithMany(h => h.PostHistories)        // Post can have many history records
                .HasForeignKey(h => h.EntityId)        // Foreign key for Post
                .OnDelete(DeleteBehavior.NoAction);    // No action when Post deleted

            modelBuilder.Entity<History<Post>>()
                .HasOne(h => h.User)                   // History has one Author
                .WithMany(h => h.PostHistories)        // User can have many history records for type Post
                .HasForeignKey(h => h.UserId)          // Foreign key for User
                .OnDelete(DeleteBehavior.NoAction);    // No action when User deleted

            modelBuilder.Entity<History<Topic>>()
                .HasOne(h => h.Entity)                 // History has one Topic
                .WithMany(h => h.TopicHistories)       // Topic can have many history records
                .HasForeignKey(h => h.EntityId)        // Foreign key for Topic
                .OnDelete(DeleteBehavior.NoAction);    // No action when Topic deleted

            modelBuilder.Entity<History<Topic>>()
                .HasOne(h => h.User)                   // History has one Author
                .WithMany(h => h.TopicHistories)       // User can have many history records for type Topic
                .HasForeignKey(h => h.UserId)          // Foreign key for User
                .OnDelete(DeleteBehavior.NoAction);    // No action when User deleted

            modelBuilder.Entity<History<Comment>>()
                .HasOne(h => h.Entity)                 // History has one Author
                .WithMany(h => h.CommentHistories)     // Comment can have many history records
                .HasForeignKey(h => h.EntityId)        // Foreign key for Comment
                .OnDelete(DeleteBehavior.NoAction);    // No action when Comment deleted

            modelBuilder.Entity<History<Comment>>()
                .HasOne(h => h.User)                   // History has one Author
                .WithMany(h => h.CommentHistories)     // User can have many history records for type Comment
                .HasForeignKey(h => h.UserId)          // Foreign key for User
                .OnDelete(DeleteBehavior.NoAction);    // No action when User deleted
        }
    }
}
