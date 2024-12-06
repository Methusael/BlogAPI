using BlogBackend.Application.Features.Posts.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Features.Posts.Mappings
{
    public class PostMapper : IEntityMapper<Post, PostDto>
    {
        public PostDto ToDto(Post post)
        {
            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                AuthorId = post.Author.Id,
                AuthorName = post.Author.Name
            };
        }
    }
}
