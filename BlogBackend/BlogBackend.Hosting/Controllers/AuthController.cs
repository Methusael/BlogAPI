using BlogBackend.Application.Features.Login.Requests;
using BlogBackend.Application.Features.Login.Responses;
using BlogBackend.Application.Features.Users.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Application.Services;
using BlogBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlogBackend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IReadService<User, UserDto> _userReadService;
        private readonly IWriteService<User, CreateUserDto, UpdateUserDto> _userWriteService;
        private readonly IConfiguration _configuration;

        public AuthController(
            IAuthService accountService,
            IReadService<User, UserDto> userReadService,
            IWriteService<User, CreateUserDto, UpdateUserDto> userWriteService,
            IConfiguration configuration)
        {
            _authService = accountService;
            _userReadService = userReadService;
            _userWriteService = userWriteService;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            var filter = (Expression<Func<User, bool>>)(u => u.Email == createUserDto.Email);
            var existingUser = (await _userReadService.GetAsync(filter)).FirstOrDefault();
            if (existingUser != null)
                return Conflict("User already exists.");

            try
            {
                createUserDto.Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
                var result = await _userWriteService.CreateAsync(createUserDto);
                return Ok("User successfully created");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create user");
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            try
            {
                string jwt = await _authService.LoginAsync(loginRequest, cancellationToken);

                Response.Cookies.Append("jwt", jwt, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    IsEssential = true,
                    Domain = "localhost"
                });
                return Ok(new
                {
                    token = jwt
                });
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid email or password");
            }
        }

        [HttpPost("Refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            var principal = GetPrincipalFromExpiredToken(refreshRequest.AccessToken);

            if (principal?.Identity?.Name is null)
                return Unauthorized();

            var filter = (Expression<Func<User, bool>>)(u => u.Email == principal.Identity.Name && u.RefreshToken == refreshRequest.RefreshToken && u.RefreshTokenExpiry > DateTime.UtcNow);
            var user = (await _userReadService.GetAsync(filter)).FirstOrDefault();

            if (user is null)
                return Unauthorized();

            var token = GenerateJwt(principal.Identity.Name);

            return Ok(new LoginResponse
            {
                JwtToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                RefreshToken = refreshRequest.RefreshToken
            });
        }

        //[Authorize]
        //[HttpDelete("Revoke")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Revoke()
        //{
        //    var email = HttpContext.User.Identity?.Name;

        //    if (email is null)
        //        return Unauthorized();

        //    var filter = (Expression<Func<User, bool>>)(u => u.Email == email);
        //    var user = (await _userReadService.GetAsync(filter)).FirstOrDefault();

        //    if (user is null)
        //        return Unauthorized();

        //    user.RefreshToken = null;

        //    await _userWriteService.UpdateAsync(user.Id, user);

        //    return Ok();
        //}

        [HttpDelete("Logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                IsEssential = true,
                Domain = "localhost"
            });

            return Ok(new
            {
                message = "Successfully logged out"
            });
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var secret = _configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured");

            var validation = new TokenValidationParameters
            {
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                ValidAudience = _configuration["JWT:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateLifetime = false
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }

        private JwtSecurityToken GenerateJwt(string email)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured")));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddSeconds(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
