using BlogBackend.Application.DTOs;
using BlogBackend.Domain.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Services
{
    public class TopicService : ITopicService
    {
        private IRepository<Topic> _repository;

        public TopicService(IRepository<Topic> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<TopicDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var topics = await _repository.GetAllAsync(cancellationToken);
            return topics.Select(topic => new TopicDTO(topic.Id, topic.Title, topic.Description)).ToList();
        }

        public async Task<IReadOnlyList<TopicDTO>> GetByIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
        {
            var posts = await _repository.GetByIdsAsync(ids, cancellationToken);
            return posts.Select(topic => new TopicDTO(topic.Id, topic.Title, topic.Description)).ToList();
        }

        public async Task<TopicDTO> GetByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(postId, cancellationToken);
        }

        public async Task<Guid> AddAsync(TopicDTO topicDto, CancellationToken cancellationToken)
        {
            if (topicDto.Id is null) throw new ArgumentNullException(nameof(topicDto.Id));

            var postToBeAdded = new Topic((Guid)topicDto.Id, topicDto.Title, topicDto.Description);

            return await _repository.AddAsync(postToBeAdded, cancellationToken);
        }

        public async Task UpdateAsync(TopicDTO topicDto, CancellationToken cancellationToken)
        {
            if (topicDto.Id is null || topicDto.AuthorId is null) throw new ArgumentNullException(nameof(topicDto.Id));

            var postToBeModified = await _repository.GetByIdAsync((Guid)topicDto.Id, cancellationToken);
            postToBeModified.Title = topicDto.Title;
            postToBeModified.Description = topicDto.Description;

            _repository.Update(postToBeModified);
        }

        public async Task DeleteByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            var postToBeDeleted = await _repository.GetByIdAsync(postId, cancellationToken);
            _repository.Delete(postToBeDeleted);
        }
    }
}
