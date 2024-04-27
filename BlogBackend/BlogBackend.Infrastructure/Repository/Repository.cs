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
            return await _targetDbSet.FindAsync(id) ?? throw new ItemNotFoundException();
        }

        public async Task<Guid> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _targetDbSet.AddAsync(entity, cancellationToken);
            return entity.Id;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _targetDbSet.Remove(entity);
        }

        public void DeleteById(Guid id)
        {
            var itemToDelete = _targetDbSet.Find(id) ?? throw new ItemNotFoundException();

            _targetDbSet.Remove(itemToDelete);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
