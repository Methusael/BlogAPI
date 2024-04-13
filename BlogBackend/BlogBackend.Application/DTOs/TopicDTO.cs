using BlogBackend.Domain.Models;

namespace BlogBackend.Application.DTOs
{
    public class TopicDTO
    {
        public Guid? Id { get; set; }

        public Guid? AuthorId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TopicDTO(Guid? id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public static implicit operator TopicDTO(Topic topic)
        {
            return new TopicDTO(topic.Id, topic.Title, topic.Description);
        }

        public override bool Equals(object? other)
        {
            if (other is not TopicDTO) return false;

            var dto = other as TopicDTO;

            if (dto is null) return false;

            return dto.Id == Id && dto.Title == Title && dto.Description == Description;

        }
    }
}
