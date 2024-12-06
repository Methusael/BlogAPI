namespace BlogBackend.Application.Interfaces
{
    public interface IEntityMapper<TEntity, TDto>
    {
        TDto ToDto(TEntity entity);
    }
}
