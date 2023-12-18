using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBackend.Domain.Models
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Post(Guid id, string title, string content)
        {
            Id = id;
            Title = title;
            Content = content;
        }

        // Navigation props
        public virtual User Author { get; set; }
    }
}
