using BlogBackend.Application.Features.Comments.Dtos;
using BlogBackend.Application.Features.Posts.Dtos;
using BlogBackend.Application.Features.Topics.Dtos;
using BlogBackend.Application.Features.Users.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Application.Services;
using BlogBackend.Domain.Models;

namespace BlogBackend.WebApi.Services
{
    internal class ReadServiceInjector
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IReadService<User, UserDto>, ReadService<User, UserDto>>();
            services.AddScoped<IReadService<Topic, TopicDto>, ReadService<Topic, TopicDto>>();
            services.AddScoped<IReadService<Post, PostDto>, ReadService<Post, PostDto>>();
            services.AddScoped<IReadService<Comment, CommentDto>, ReadService<Comment, CommentDto>>();
        }
    }
}
