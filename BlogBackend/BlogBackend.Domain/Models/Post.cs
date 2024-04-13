using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogBackend.Domain.Models
{
    public class Post : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public ICollection<Post> Posts { get; } = new List<Post>();

        [ForeignKey("TopicId")]
        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }

        public Post(Guid id, string title, string content, Guid topicId)
        {
            Id = id;
            Title = title;
            Content = content;
            TopicId = topicId;
        }
    }
}
