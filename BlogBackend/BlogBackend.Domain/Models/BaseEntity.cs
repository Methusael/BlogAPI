using System.ComponentModel.DataAnnotations;

namespace BlogBackend.Domain.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
