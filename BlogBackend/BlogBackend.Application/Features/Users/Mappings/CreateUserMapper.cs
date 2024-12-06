using BlogBackend.Application.Features.Users.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Enums;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Features.Users.Mappings
{
    public class CreateUserMapper : ICreateEntityMapper<User, CreateUserDto>
    {
        public User ToEntity(Guid id, CreateUserDto dto)
        {
            return new User()
            {
                Id = id,
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Role = RoleType.User,
                CreatedDate = DateTime.UtcNow,
                RefreshToken = string.Empty,
                RefreshTokenExpiry = DateTime.UtcNow,
            };
        }
    }
}
