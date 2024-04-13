using BlogBackend.Domain.Models;
namespace BlogBackend.Application.DTOs
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid TopicId { get; set; }

        public PostDTO(Guid id, string title, string content, Guid topicId)
        {
            Id = id;
            Title = title;
            Content = content;
            TopicId = topicId;
        }

        public static implicit operator Post(PostDTO dto)
        {
            return new Post(dto.Id, dto.Title, dto.Content, dto.TopicId);
        }

        public static implicit operator PostDTO(Post post)
        {
            return new PostDTO(post.Id, post.Title, post.Content, post.TopicId);
        }

        public override bool Equals(object? other)
        {
            return other is PostDTO && other.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, Content);
        }
    }
}
