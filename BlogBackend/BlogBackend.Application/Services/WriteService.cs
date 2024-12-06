using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Services
{
    public class WriteService<TEntity, TCreateDto, TUpdateDto> : IWriteService<TEntity, TCreateDto, TUpdateDto>
        where TEntity : BaseEntity
        where TCreateDto : class
        where TUpdateDto : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly ICreateEntityMapper<TEntity, TCreateDto> _createMapper;
        private readonly IUpdateEntityMapper<TEntity, TUpdateDto> _updateMapper;

        public WriteService(
            IGenericRepository<TEntity> repository,
            ICreateEntityMapper<TEntity, TCreateDto> createMapper,
            IUpdateEntityMapper<TEntity, TUpdateDto> updateMapper)
        {
            _repository = repository;
            _createMapper = createMapper;
            _updateMapper = updateMapper;
        }

        public async Task<bool> CreateAsync(TCreateDto createDto)
        {
            if (createDto == null)
                throw new ArgumentNullException(nameof(createDto));

            Guid id = Guid.NewGuid();

            while (await _repository.GetByIdAsync(id) != null)
                id = Guid.NewGuid();

            var entity = _createMapper.ToEntity(id, createDto);

            await _repository.InsertAsync(entity);
            return await _repository.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Guid id, TUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }
               
            _updateMapper.UpdateEntity(entity, dto);
            _repository.Update(entity);

            return await _repository.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return await _repository.SaveChangesAsync() > 0;
        }
    }
}
