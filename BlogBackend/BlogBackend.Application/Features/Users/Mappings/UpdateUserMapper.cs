using BlogBackend.Application.Features.Users.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Features.Users.Mappings
{
    public class UpdateUserMapper : IUpdateEntityMapper<User, UpdateUserDto>
    {
        public void UpdateEntity(User entity, UpdateUserDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
