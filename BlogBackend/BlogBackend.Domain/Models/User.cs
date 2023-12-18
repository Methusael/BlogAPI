using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBackend.Domain.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public User(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
