namespace BlogBackend.Domain.Models
{
    public class Topic : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        // Foreign key for the User
        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public ICollection<History<Topic>> TopicHistories { get; } = new List<History<Topic>>();
        public ICollection<Post> Posts { get; set; }
    }
}
