using BlogBackend.Application.DTOs;
using BlogBackend.Application.DTOs.Requests;

namespace BlogBackend.Application.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken);

        Task<UserDTO> RegisterAsync(RegisterRequest registerRequest, CancellationToken cancellationToken);
    }
}
