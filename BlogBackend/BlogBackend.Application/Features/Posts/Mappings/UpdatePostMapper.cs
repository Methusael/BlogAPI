using BlogBackend.Application.Features.Posts.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Features.Posts.Mappings
{
    public class UpdatePostMapper : IUpdateEntityMapper<Post, UpdatePostDto>
    {
        public void UpdateEntity(Post entity, UpdatePostDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
