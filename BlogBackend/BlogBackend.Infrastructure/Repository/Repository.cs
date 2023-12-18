using BlogBackend.Domain.Exceptions;
using BlogBackend.Domain.Interfaces;
using BlogBackend.Domain.Models;
using BlogBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogBackend.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _targetDbSet;
        private readonly DbContext _dbContext;

        public Repository(ApplicationDbContext context)
        {
            _dbContext = context;
            _targetDbSet = _dbContext.Set<T>();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _targetDbSet.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetByIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
        {
            return await _targetDbSet.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken) ?? throw new ItemNotFoundException();
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _targetDbSet.FindAsync(id, cancellationToken) ?? throw new ItemNotFoundException();
        }

        public async Task<Guid> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _targetDbSet.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task UpdateAsync(T entity,CancellationToken cancellationToken)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _targetDbSet.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
