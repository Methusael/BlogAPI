using BlogBackend.Application.Features.Comments.Dtos;
using BlogBackend.Application.Features.Comments.Mappings;
using BlogBackend.Application.Features.Posts.Dtos;
using BlogBackend.Application.Features.Posts.Mappings;
using BlogBackend.Application.Features.Topics.Dtos;
using BlogBackend.Application.Features.Topics.Mappings;
using BlogBackend.Application.Features.Users.Dtos;
using BlogBackend.Application.Features.Users.Mappings;
using BlogBackend.Application.Interfaces;
using BlogBackend.Application.Services;
using BlogBackend.Domain.Interfaces;
using BlogBackend.Domain.Models;
using BlogBackend.Infrastructure.Data;
using BlogBackend.Infrastructure.Repository;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;

using System.Text;

namespace BlogBackend.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            if (!builder.Environment.IsEnvironment("Testing"))
            {
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
            }

            Console.WriteLine("Adding logging.");
            builder.Services.AddLogging(options =>
            {
                options.AddConsole();
                options.AddDebug();
            });

            Console.WriteLine("Adding controllers.");
            builder.Services.AddControllers();

            Console.WriteLine("Configure authentication.");
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kPwuwFQxv2GGTAga3wMeMAjTfWlGJ4o3m11OqMUcbQuJ5Dw90Td6YyFFZI5zKDLfWCh4jrVp3zqOtKrzJlMRdYiqu1YSxsPc7fGh"))
                };
            });

            Console.WriteLine("Configure authorization.");
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            Console.WriteLine("Adding repositories.");
            builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            builder.Services.AddScoped<IGenericRepository<Topic>, GenericRepository<Topic>>();
            builder.Services.AddScoped<IGenericRepository<Post>, GenericRepository<Post>>();
            builder.Services.AddScoped<IGenericRepository<Comment>, GenericRepository<Comment>>();

            Console.WriteLine("Adding mappers.");
            builder.Services.AddTransient<IEntityMapper<User, UserDto>, UserMapper>();
            builder.Services.AddTransient<IEntityMapper<Topic, TopicDto>, TopicMapper>();
            builder.Services.AddTransient<IEntityMapper<Post, PostDto>, PostMapper>();
            builder.Services.AddTransient<IEntityMapper<Comment, CommentDto>, CommentMapper>();

            builder.Services.AddTransient<ICreateEntityMapper<User, CreateUserDto>, CreateUserMapper>();
            builder.Services.AddTransient<ICreateEntityMapper<Topic, CreateTopicDto>, CreateTopicMapper>();
            builder.Services.AddTransient<ICreateEntityMapper<Post, CreatePostDto>, CreatePostMapper>();
            builder.Services.AddTransient<ICreateEntityMapper<Comment, CreateCommentDto>, CreateCommentMapper>();

            builder.Services.AddTransient<IUpdateEntityMapper<User, UpdateUserDto>, UpdateUserMapper>();
            builder.Services.AddTransient<IUpdateEntityMapper<Topic, UpdateTopicDto>, UpdateTopicMapper>();
            builder.Services.AddTransient<IUpdateEntityMapper<Post, UpdatePostDto>, UpdatePostMapper>();
            builder.Services.AddTransient<IUpdateEntityMapper<Comment, UpdateCommentDto>, UpdateCommentMapper>();

            Console.WriteLine("Adding services.");
            //builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IReadService<User, UserDto>, ReadService<User, UserDto>>();
            builder.Services.AddScoped<IReadService<Topic, TopicDto>, ReadService<Topic, TopicDto>>();
            builder.Services.AddScoped<IReadService<Post, PostDto>, ReadService<Post, PostDto>>();
            builder.Services.AddScoped<IReadService<Comment, CommentDto>, ReadService<Comment, CommentDto>>();

            builder.Services.AddScoped<IWriteService<User, CreateUserDto, UpdateUserDto>, WriteService<User, CreateUserDto, UpdateUserDto>>();
            builder.Services.AddScoped<IWriteService<Topic, CreateTopicDto, UpdateTopicDto>, WriteService<Topic, CreateTopicDto, UpdateTopicDto>>();
            builder.Services.AddScoped<IWriteService<Post, CreatePostDto, UpdatePostDto>, WriteService<Post, CreatePostDto, UpdatePostDto>>();
            builder.Services.AddScoped<IWriteService<Comment, CreateCommentDto, UpdateCommentDto>, WriteService<Comment, CreateCommentDto, UpdateCommentDto>>();

            Console.WriteLine("Done.");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin",
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    }
                );
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (builder.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.Run();
        }
    }
}