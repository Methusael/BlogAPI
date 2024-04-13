using System.ComponentModel.DataAnnotations.Schema;

namespace BlogBackend.Domain.Models
{
    public class TopicHistory : BaseHistory
    {
        [ForeignKey(nameof(TopicId))]
        public Guid TopicId { get; set; }

        /// <summary>
        /// Navigatiton property
        /// </summary>
        public Topic Topic { get; set; }

        public ICollection<TopicHistory> TopicHistories { get; } = new List<TopicHistory>();
    }
}
