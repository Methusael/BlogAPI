using BlogBackend.Application.Features.Topics.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Features.Topics.Mappings
{
    public class TopicMapper : IEntityMapper<Topic, TopicDto>
    {
        public TopicDto ToDto(Topic post)
        {
            return new TopicDto
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                AuthorId = post.Author.Id,
                AuthorName = post.Author.Name
            };
        }
    }
}
