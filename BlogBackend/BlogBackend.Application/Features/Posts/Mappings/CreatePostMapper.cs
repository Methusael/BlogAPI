using BlogBackend.Application.Features.Posts.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Features.Posts.Mappings
{
    public class CreatePostMapper : ICreateEntityMapper<Post, CreatePostDto>
    {
        public Post ToEntity(Guid id, CreatePostDto dto)
        {
            return new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                AuthorId = dto.AuthorId,
                CreatedDate = DateTime.UtcNow
            };
        }
    }
}
