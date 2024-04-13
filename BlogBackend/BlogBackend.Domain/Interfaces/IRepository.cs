using BlogBackend.Domain.Models;

namespace BlogBackend.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<T>> GetByIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken);

        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<Guid> AddAsync(T entity, CancellationToken cancellationToken);

        void Update(T entity);

        void Delete(T entity);

        void DeleteById(Guid id);

        void SaveChanges();

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
