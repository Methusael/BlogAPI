using BlogBackend.Application.Features.Comments.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Features.Comments.Mappings
{
    public class CreateCommentMapper : ICreateEntityMapper<Comment, CreateCommentDto>
    {
        public Comment ToEntity(Guid id, CreateCommentDto dto)
        {
            return new Comment
            {
                Text = dto.Text,
                CreatedDate = DateTime.UtcNow,
                AuthorId = dto.UserId,
                PostId = dto.PostId,
            };
        }
    }
}
