using BlogBackend.Application.Features.Login.Requests;
using BlogBackend.Application.Features.Users.Dtos;

namespace BlogBackend.Application.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken);
    }
}
