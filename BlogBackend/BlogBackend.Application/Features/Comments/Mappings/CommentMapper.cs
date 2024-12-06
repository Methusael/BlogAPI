using BlogBackend.Application.Features.Comments.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Features.Comments.Mappings
{
    public class CommentMapper : IEntityMapper<Comment, CommentDto>
    {
        public CommentDto ToDto(Comment post)
        {
            return new CommentDto
            {
                Id = post.Id,
                Text = post.Text,
            };
        }
    }
}
