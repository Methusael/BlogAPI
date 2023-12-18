using BlogBackend.Domain.Models;

namespace BlogBackend.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<T>> GetByIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken);

        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<Guid> AddAsync(T entity, CancellationToken cancellationToken);

        Task UpdateAsync(T entity, CancellationToken cancellationToken);

        Task DeleteAsync(T entity, CancellationToken cancellationToken);
    }
}
