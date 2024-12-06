using BlogBackend.Domain.Interfaces;
using BlogBackend.Domain.Models;
using BlogBackend.Infrastructure.Repository;

namespace BlogBackend.WebApi.Services
{
    internal class RepositoryInjector
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<Topic>, GenericRepository<Topic>>();
            services.AddScoped<IGenericRepository<Post>, GenericRepository<Post>>();
            services.AddScoped<IGenericRepository<Comment>, GenericRepository<Comment>>();
        }
    }
}
