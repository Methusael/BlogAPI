using System.ComponentModel.DataAnnotations.Schema;

namespace BlogBackend.Domain.Models
{
    public class PostHistory : BaseHistory
    {
        [ForeignKey(nameof(PostId))]
        public Guid PostId { get; set; }

        /// <summary>
        /// Navigation property
        /// </summary>
        public Post Post { get; set; }

        public ICollection<PostHistory> PostHistories { get; } = new List<PostHistory>();
    }
}
