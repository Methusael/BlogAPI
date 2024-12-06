//using BlogBackend.Application.DTOs;
//using BlogBackend.Application.Features.Login.Requests;
//using BlogBackend.Domain.Enums;
//using BlogBackend.Domain.Exceptions;
//using BlogBackend.Domain.Interfaces;
//using BlogBackend.Domain.Models;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace BlogBackend.Application.Services
//{
//    public class AuthService : IAuthService
//    {
//        private readonly IGenericRepository<User> _userRepository;

//        private readonly IConfiguration _configuration;

//        public AuthService(IGenericRepository<User> userRepository, IConfiguration configuration)
//        {
//            _userRepository = userRepository;
//            _configuration = configuration;
//        }

//        public async Task<string> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
//        {
//            var users = await _userRepository.GetAllAsync(cancellationToken);
//            var user = users.FirstOrDefault(x => x.Email == loginRequest.Email);

//            if (user == null)
//            {
//                throw new ItemNotFoundException("No such user");
//            }

//            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
//            {
//                Console.WriteLine("------------------------ Bad password");
//                throw new AccessViolationException("Bad password");
//            }

//            string jwt = GenerateAccessToken(user);

//            return jwt;
//        }

//        public async Task<UserDTO> RegisterAsync(RegisterRequest registerRequest, CancellationToken cancellationToken)
//        {
//            var list = await _userRepository.GetAllAsync(cancellationToken);
//            var userExists = list.FirstOrDefault(x => x.Email == registerRequest.Email) is null ? false : true;
//            if (userExists) throw new InvalidOperationException(nameof(registerRequest.Email));

//            bool success = false;
//            Guid id;
//            do
//            {
//                id = Guid.NewGuid();
//                try
//                {
//                    await _userRepository.GetByIdAsync(id, cancellationToken);
//                }
//                catch (ItemNotFoundException ex)
//                {
//                    success = true;
//                }
//            } while (!success);

//            registerRequest.Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

//            User user = new User(id, registerRequest.Email, registerRequest.Password, RoleType.User);
//            await _userRepository.AddAsync(user, cancellationToken);

//            UserDTO userDTO = new UserDTO();
//            userDTO.Email = registerRequest.Email;
//            userDTO.Role = RoleType.User;

//            _userRepository.SaveChanges();

//            return userDTO;
//        }

//        internal string GenerateAccessToken(User user)
//        {
//            List<Claim> claims = new List<Claim> {
//                new Claim(ClaimTypes.Name, user.Email),
//                new Claim(ClaimTypes.Role, user.Role.ToString())
//            };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kPwuwFQxv2GGTAga3wMeMAjTfWlGJ4o3m11OqMUcbQuJ5Dw90Td6YyFFZI5zKDLfWCh4jrVp3zqOtKrzJlMRdYiqu1YSxsPc7fGh"));

//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

//            var token = new JwtSecurityToken(
//                    claims: claims,
//                    expires: DateTime.Now.AddDays(1),
//                    signingCredentials: creds
//                );

//            string jwt = string.Empty;
//            try
//            {
//                jwt = new JwtSecurityTokenHandler().WriteToken(token);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//            }

//            return jwt;
//        }
//    }
//}
