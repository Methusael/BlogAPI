namespace BlogBackend.Application.Interfaces
{
    public interface IUpdateEntityMapper<TEntity, TUpdateDto>
        where TEntity : class
        where TUpdateDto : class
    {
        void UpdateEntity(TEntity entity, TUpdateDto dto);
    }
}
