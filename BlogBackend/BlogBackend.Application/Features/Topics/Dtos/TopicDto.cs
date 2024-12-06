namespace BlogBackend.Application.Features.Topics.Dtos
{
    public class TopicDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}