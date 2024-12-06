using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Interfaces
{
    public interface IWriteService<TEntity, TCreateDto, TUpdateDto>
        where TEntity : BaseEntity
        where TCreateDto : class
        where TUpdateDto : class
    {
        Task<bool> CreateAsync(TCreateDto dto);
        Task<bool> UpdateAsync(Guid id, TUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
