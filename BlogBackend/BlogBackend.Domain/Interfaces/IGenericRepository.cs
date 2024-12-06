using BlogBackend.Domain.Models;
using System.Linq.Expressions;

namespace BlogBackend.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<TEntity> GetByIdAsync(Guid id);

        Task InsertAsync(TEntity entity);

        Task DeleteAsync(Guid id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);

        Task<int> SaveChangesAsync();

        int SaveChanges();
    }
}
