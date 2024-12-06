using BlogBackend.Application.Features.Topics.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Features.Topics.Mappings
{
    public class CreateTopicMapper : ICreateEntityMapper<Topic, CreateTopicDto>
    {
        public Topic ToEntity(Guid id, CreateTopicDto dto)
        {
            return new Topic()
            {
                Id = id,
                Title = dto.Title,
                Description = dto.Description,
                AuthorId = dto.AuthorId
            };
        }
    }
}
