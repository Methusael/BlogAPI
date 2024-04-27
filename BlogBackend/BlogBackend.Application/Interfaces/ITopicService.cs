using BlogBackend.Application.DTOs;

namespace BlogBackend.Application.Services
{
    public interface ITopicService
    {
        Task<IReadOnlyList<TopicDTO>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<TopicDTO>> GetByIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken);

        Task<TopicDTO> GetByIdAsync(Guid topicId, CancellationToken cancellationToken);

        Task<Guid> AddAsync(TopicDTO topicDTO, CancellationToken cancellationToken);

        Task UpdateAsync(TopicDTO topicDTO, CancellationToken cancellationToken);

        Task DeleteByIdAsync(Guid topicId, CancellationToken cancellationToken);
    }
}
