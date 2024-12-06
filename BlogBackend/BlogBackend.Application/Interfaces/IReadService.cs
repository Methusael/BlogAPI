using System.Linq.Expressions;

namespace BlogBackend.Application.Interfaces
{
    public interface IReadService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        Task<IEnumerable<TDto>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(Guid id);
    }
}
