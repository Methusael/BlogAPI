using BlogBackend.Application.Features.Login.Requests;
using BlogBackend.Domain.Interfaces;
using BlogBackend.Domain.Models;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace BlogBackend.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepository;

        public AuthService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var filter = (Expression<Func<User, bool>>)(u => u.Email == loginRequest.Email);
            var user = (await _userRepository.GetAsync(filter)).FirstOrDefault();

            if (user == null)
            {
                return string.Empty;
            }

            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                Console.WriteLine("------------------------ Bad password");
                throw new AccessViolationException("Bad password");
            }

            string jwt = GenerateAccessToken(user);

            return jwt;
        }

        internal string GenerateAccessToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kPwuwFQxv2GGTAga3wMeMAjTfWlGJ4o3m11OqMUcbQuJ5Dw90Td6YyFFZI5zKDLfWCh4jrVp3zqOtKrzJlMRdYiqu1YSxsPc7fGh"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            string jwt = string.Empty;
            try
            {
                jwt = new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return jwt;
        }
    }
}
