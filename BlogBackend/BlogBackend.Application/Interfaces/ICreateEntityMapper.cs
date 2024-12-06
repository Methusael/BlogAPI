namespace BlogBackend.Application.Interfaces
{
    public interface ICreateEntityMapper<TEntity, TCreateDto>
        where TEntity : class
        where TCreateDto : class
    {
        TEntity ToEntity(Guid id, TCreateDto dto);
    }
}
