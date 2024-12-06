using BlogBackend.Application.Features.Comments.Dtos;
using BlogBackend.Application.Features.Posts.Dtos;
using BlogBackend.Application.Features.Topics.Dtos;
using BlogBackend.Application.Features.Users.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Application.Services;
using BlogBackend.Domain.Models;

namespace BlogBackend.WebApi.Services
{
    internal class WriteServiceInjector
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IWriteService<User, CreateUserDto, UpdateUserDto>, WriteService<User, CreateUserDto, UpdateUserDto>>();
            services.AddScoped<IWriteService<Topic, CreateTopicDto, UpdateTopicDto>, WriteService<Topic, CreateTopicDto, UpdateTopicDto>>();
            services.AddScoped<IWriteService<Post, CreatePostDto, UpdatePostDto>, WriteService<Post, CreatePostDto, UpdatePostDto>>();
            services.AddScoped<IWriteService<Comment, CreateCommentDto, UpdateCommentDto>, WriteService<Comment, CreateCommentDto, UpdateCommentDto>>();
        }
    }
}
