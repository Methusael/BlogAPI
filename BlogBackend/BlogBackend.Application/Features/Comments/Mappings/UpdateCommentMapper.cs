using BlogBackend.Application.Features.Comments.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Features.Comments.Mappings
{
    public class UpdateCommentMapper : IUpdateEntityMapper<Comment, UpdateCommentDto>
    {
        public void UpdateEntity(Comment entity, UpdateCommentDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
