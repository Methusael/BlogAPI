﻿namespace BlogBackend.Application.Features.Posts.Dtos
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
    }
}
