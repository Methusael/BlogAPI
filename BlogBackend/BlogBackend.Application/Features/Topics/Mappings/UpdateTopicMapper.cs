using BlogBackend.Application.Features.Topics.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Features.Topics.Mappings
{
    public class UpdateTopicMapper : IUpdateEntityMapper<Topic, UpdateTopicDto>
    {
        public void UpdateEntity(Topic entity, UpdateTopicDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
