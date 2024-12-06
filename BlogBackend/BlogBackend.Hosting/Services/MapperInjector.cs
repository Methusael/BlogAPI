using BlogBackend.Application.Features.Comments.Dtos;
using BlogBackend.Application.Features.Comments.Mappings;
using BlogBackend.Application.Features.Posts.Dtos;
using BlogBackend.Application.Features.Posts.Mappings;
using BlogBackend.Application.Features.Topics.Dtos;
using BlogBackend.Application.Features.Topics.Mappings;
using BlogBackend.Application.Features.Users.Dtos;
using BlogBackend.Application.Features.Users.Mappings;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.WebApi.Services
{
    internal class MapperInjector
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IEntityMapper<User, UserDto>, UserMapper>();
            services.AddTransient<IEntityMapper<Topic, TopicDto>, TopicMapper>();
            services.AddTransient<IEntityMapper<Post, PostDto>, PostMapper>();
            services.AddTransient<IEntityMapper<Comment, CommentDto>, CommentMapper>();

            services.AddTransient<ICreateEntityMapper<User, CreateUserDto>, CreateUserMapper>();
            services.AddTransient<ICreateEntityMapper<Topic, CreateTopicDto>, CreateTopicMapper>();
            services.AddTransient<ICreateEntityMapper<Post, CreatePostDto>, CreatePostMapper>();
            services.AddTransient<ICreateEntityMapper<Comment, CreateCommentDto>, CreateCommentMapper>();

            services.AddTransient<IUpdateEntityMapper<User, UpdateUserDto>, UpdateUserMapper>();
            services.AddTransient<IUpdateEntityMapper<Topic, UpdateTopicDto>, UpdateTopicMapper>();
            services.AddTransient<IUpdateEntityMapper<Post, UpdatePostDto>, UpdatePostMapper>();
            services.AddTransient<IUpdateEntityMapper<Comment, UpdateCommentDto>, UpdateCommentMapper>();
        }
    }
}
