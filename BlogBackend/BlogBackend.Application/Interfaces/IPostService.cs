using BlogBackend.Application.DTOs;

namespace BlogBackend.Application.Services
{
    public interface IPostService
    {
        Task<IReadOnlyList<PostDTO>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<PostDTO>> GetByIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken);

        Task<PostDTO> GetByIdAsync(Guid postId, CancellationToken cancellationToken);

        Task<Guid> AddAsync(PostDTO postDTO, CancellationToken cancellationToken);

        Task UpdateAsync(PostDTO postDTO, CancellationToken cancellationToken);

        Task DeleteByIdAsync(Guid postId, CancellationToken cancellationToken);
    }
}
