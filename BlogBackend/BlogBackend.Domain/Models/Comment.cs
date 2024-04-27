using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogBackend.Domain.Models
{
    public class Comment : BaseEntity
    {
        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }

        /// <summary>
        /// Navigation property
        /// </summary>
        public User User { get; set; }

        public string Text { get; set; }

        [ForeignKey(nameof(PostId))]
        public Guid PostId { get; set; }

        /// <summary>
        /// Navigation property
        /// </summary>
        public Topic Post { get; set; }
    }
}
