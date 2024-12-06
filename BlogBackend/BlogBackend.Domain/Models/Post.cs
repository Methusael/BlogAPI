namespace BlogBackend.Domain.Models
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        // Foreign key for the User
        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        // Foreign key for the Topic
        public Guid TopicId { get; set; }
        public Topic Topic { get; set; }

        public ICollection<History<Post>> PostHistories { get; } = new List<History<Post>>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
