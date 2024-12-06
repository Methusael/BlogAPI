namespace BlogBackend.Application.Features.Topics.Dtos
{
    public class CreateTopicDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
    }
}
