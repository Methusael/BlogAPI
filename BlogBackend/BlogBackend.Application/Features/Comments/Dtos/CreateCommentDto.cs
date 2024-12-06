namespace BlogBackend.Application.Features.Comments.Dtos
{
    public class CreateCommentDto
    {
        public string Text { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
