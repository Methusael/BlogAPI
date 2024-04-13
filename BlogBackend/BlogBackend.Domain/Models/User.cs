using BlogBackend.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlogBackend.Domain.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }        

        public ICollection<Comment> Comments { get; } = new List<Comment>();

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiry { get; set; }

        public User(Guid id, string name, string email, Role role)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
        }
    }
}