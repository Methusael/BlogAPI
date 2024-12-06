using BlogBackend.Domain.Enums;

namespace BlogBackend.Domain.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password{ get; set; }
        public RoleType Role { get; set; }
        public DateTime CreatedAt { get; set; }        

        public ICollection<Topic> Topics { get; } = new List<Topic>();
        public ICollection<Post> Posts { get; } = new List<Post>();
        public ICollection<Comment> Comments { get; } = new List<Comment>();

        public ICollection<History<Topic>> TopicHistories { get; } = new List<History<Topic>>();
        public ICollection<History<Post>> PostHistories { get; } = new List<History<Post>>();
        public ICollection<History<Comment>> CommentHistories { get; } = new List<History<Comment>>();


        //public string? RefreshToken { get; set; }

        //public DateTime? RefreshTokenExpiry { get; set; }
    }
}