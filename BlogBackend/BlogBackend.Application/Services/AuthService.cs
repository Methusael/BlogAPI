using BlogBackend.Application.DTOs;
using BlogBackend.Application.DTOs.Requests;
using BlogBackend.Domain.Enums;
using BlogBackend.Domain.Exceptions;
using BlogBackend.Domain.Interfaces;
using BlogBackend.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BlogBackend.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly string secureKey = "valami nagyonszupereroscucclikey";

        private readonly IRepository<User> _userRepository;

        public AuthService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);
            var user = users.FirstOrDefault(x => x.Email == loginRequest.Email);

            if (user == null)
            {
                throw new ItemNotFoundException("No such user");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                throw new AccessViolationException("Bad password");
            }

            string jwt = GenerateToken(user.Id);

            return jwt;
        }

        public async Task<UserDTO> RegisterAsync(RegisterRequest registerRequest, CancellationToken cancellationToken)
        {
            var list = await _userRepository.GetAllAsync(cancellationToken);
            var userExists = list.FirstOrDefault(x => x.Email == registerRequest.Email) is null ? false : true;
            if (userExists) throw new InvalidOperationException(nameof(registerRequest.Email));

            bool success = false;
            Guid id;
            do
            {
                id = Guid.NewGuid();
                try
                {
                    await _userRepository.GetByIdAsync(id, cancellationToken);
                }
                catch (ItemNotFoundException ex)
                {
                    success = true;
                }
            } while (!success);

            registerRequest.Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

            User user = new User(id, registerRequest.Email, registerRequest.Password, Role.User);
            await _userRepository.AddAsync(user, cancellationToken);

            UserDTO userDTO = new UserDTO();
            userDTO.Email = registerRequest.Email;
            userDTO.Role = Role.User;

            _userRepository.SaveChanges();

            return userDTO;
        }


        internal string GenerateToken(Guid id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        internal JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
