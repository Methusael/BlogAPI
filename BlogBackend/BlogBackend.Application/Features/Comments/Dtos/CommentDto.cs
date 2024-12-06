namespace BlogBackend.Application.Features.Comments.Dtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
