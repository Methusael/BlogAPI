﻿namespace BlogBackend.Domain.Models
{
    public class History<TEntity> : BaseEntity
    {
        public string Action { get; set; }
        public DateTime CreatedDate { get; set; }

        // Foreign key for the User
        public Guid UserId { get; set; }
        public User User { get; set; }

        // Foreign key for the entity (Post, Topic, Comment)
        public Guid EntityId { get; set; }
        public TEntity Entity { get; set; }
    }
}
