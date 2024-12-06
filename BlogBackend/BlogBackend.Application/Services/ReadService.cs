using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Interfaces;
using BlogBackend.Domain.Models;

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

        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity != null ? _readMapper.ToDto(entity) : null;
        }
    }
}
