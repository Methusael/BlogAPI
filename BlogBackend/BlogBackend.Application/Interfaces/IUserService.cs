using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Services
{
    public interface IUserService
    {
        Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
