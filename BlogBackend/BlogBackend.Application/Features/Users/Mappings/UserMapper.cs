using BlogBackend.Application.Features.Users.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Features.Users.Mappings
{
    public class UserMapper : IEntityMapper<User, UserDto>
    {
        public UserDto ToDto(User post)
        {
            return new UserDto
            {
                Id = post.Id,
                Name = post.Name,
            };
        }
    }
}
