using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogBackend.Domain.Models
{
    public class BaseHistory : BaseEntity
    {
        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }

        /// <summary>
        /// Navigation property
        /// </summary>
        public User User { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public DateTime When { get; set; }
    }
}
