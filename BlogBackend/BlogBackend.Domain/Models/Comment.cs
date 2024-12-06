namespace BlogBackend.Domain.Models
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }

        // Foreign key for the User
        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        // Foreign key for the Post
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public ICollection<History<Comment>> CommentHistories { get; } = new List<History<Comment>>();
    }
}