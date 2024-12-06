namespace BlogBackend.Application.Features.History
{
    public class HistoryDto<TEntity>
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public string Action { get; set; } // e.g., "Created", "Updated", "Deleted"
        public DateTime Timestamp { get; set; }
        public TEntity Entity { get; set; }
    }
}
