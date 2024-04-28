using BlogBackend.Domain.Enums;

namespace BlogBackend.Application.DTOs
{
    [Serializable]
    public class UserDTO
    {
        public string Email { get; set; }

        public RoleType Role { get; set; }
    }
}
