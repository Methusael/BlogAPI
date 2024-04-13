using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Services
{
    public interface IAccountService
    {
        Task<User> RegisterAsync(string username, string password, CancellationToken cancellationToken);

        Task<User> AuthenticateAsync(string username, string password, CancellationToken cancellationToken);

        Task LogoutAsync(string username, CancellationToken cancellationToken);

        Task<string> RefreshTokenAsync(string username, CancellationToken cancellationToken);

        Task<User> FindByUsernameAsync(string username, CancellationToken cancellationToken);

        Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken);

        Task<bool> CheckPasswordAsync(User user, string password);

        void Update(User user);
    }
}
