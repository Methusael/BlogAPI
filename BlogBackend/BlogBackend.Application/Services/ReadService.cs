using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Interfaces;
using BlogBackend.Domain.Models;
using System.Linq.Expressions;

namespace BlogBackend.Application.Services
{
    public class ReadService<TEntity, TDto> : IReadService<TEntity, TDto>
        where TEntity : BaseEntity
        where TDto : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IEntityMapper<TEntity, TDto> _readMapper;

        public ReadService(IGenericRepository<TEntity> repository, IEntityMapper<TEntity, TDto> mapper)
        {
            _repository = repository;
            _readMapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            IEnumerable<TEntity> result = await _repository.GetAsync();
            return result.Select(e => _readMapper.ToDto(e));
        }

        public async Task<IEnumerable<TDto>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IEnumerable<TEntity> res = await _repository.GetAsync(filter, orderBy, includeProperties);
            IReadOnlyList<TDto> mappedResult = res.Select(entity => _readMapper.ToDto(entity)).ToList();
            return mappedResult;
        }

        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity != null ? _readMapper.ToDto(entity) : null;
        }
    }
}
