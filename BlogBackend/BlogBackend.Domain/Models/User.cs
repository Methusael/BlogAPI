using BlogBackend.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlogBackend.Domain.Models
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password{ get; set; }

        [Required]
        public RoleType Role { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }        

        public ICollection<Comment> Comments { get; } = new List<Comment>();

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; }

        public User(Guid id, string email, string password, RoleType role)
        {
            Id = id;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}