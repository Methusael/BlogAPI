//using BlogBackend.Application.DTOs;
//using BlogBackend.Application.Features.Login.Requests;
//using BlogBackend.Application.Features.Login.Responses;
//using BlogBackend.Application.Services;
//using BlogBackend.Domain.Exceptions;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;

//namespace BlogBackend.WebApi.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class AuthController : ControllerBase
//    {
//        private readonly IAuthService _authService;
//        private readonly IUserService _userService;
//        private readonly IConfiguration _configuration;
//        private readonly ILogger<AuthController> _logger;

//        public AuthController(IAuthService accountService,IUserService userService, IConfiguration configuration, ILogger<AuthController> logger)
//        {
//            _authService = accountService;
//            _userService = userService;
//            _configuration = configuration;
//            _logger = logger;
//        }

//        [HttpPost("Register")]
//        [ProducesResponseType(StatusCodes.Status409Conflict)]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> Register([FromBody] RegisterRequest model, CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("Register called");

//            try
//            {
//                var existingUser = await _userService.FindByEmailAsync(model.Email, cancellationToken);
//                if (existingUser != null)
//                    return Conflict("User already exists.");
//            }
//            catch (ItemNotFoundException)
//            {
//            }            

//            try
//            {
//                var result = await _authService.RegisterAsync(model, cancellationToken);
//                _logger.LogInformation("Register succeeded");
//                return Ok("User successfully created");
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create user");
//            }
//        }

//        [HttpPost("Login")]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
//        {
//            try
//            {
//                string jwt = await _authService.LoginAsync(loginRequest, cancellationToken);

//                Response.Cookies.Append("jwt", jwt, new CookieOptions
//                {
//                    HttpOnly = true,
//                    SameSite = SameSiteMode.None,
//                    Secure = true,
//                    IsEssential = true,
//                    Domain = "localhost"
//                });
//                return Ok(new
//                {
//                    token = jwt
//                });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest("Invalid email or password");
//            }
//        }

//        //[HttpPost("Refresh")]
//        //[ProducesResponseType(StatusCodes.Status200OK)]
//        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        //public async Task<IActionResult> Refresh([FromBody] RefreshRequest model)
//        //{
//        //    _logger.LogInformation("Refresh called");

//        //    var principal = GetPrincipalFromExpiredToken(model.AccessToken);

//        //    if (principal?.Identity?.Name is null)
//        //        return Unauthorized();

//        //    var user = await _userService.FindByEmailAsync(principal.Identity.Name, CancellationToken.None);

//        //    if (user is null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
//        //        return Unauthorized();

//        //    var token = GenerateJwt(principal.Identity.Name);

//        //    _logger.LogInformation("Refresh succeeded");

//        //    return Ok(new LoginResponse
//        //    {
//        //        JwtToken = new JwtSecurityTokenHandler().WriteToken(token),
//        //        Expiration = token.ValidTo,
//        //        RefreshToken = model.RefreshToken
//        //    });
//        //}

//        //[Authorize]
//        //[HttpDelete("Revoke")]
//        //[ProducesResponseType(StatusCodes.Status200OK)]
//        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        //public async Task<IActionResult> Revoke()
//        //{
//        //    _logger.LogInformation("Revoke called");

//        //    var email = HttpContext.User.Identity?.Name;

//        //    if (email is null)
//        //        return Unauthorized();

//        //    var user = await _userService.FindByEmailAsync(email, CancellationToken.None);

//        //    if (user is null)
//        //        return Unauthorized();

//        //    user.RefreshToken = null;

//        //    _authService.Update(user);

//        //    _logger.LogInformation("Revoke succeeded");

//        //    return Ok();
//        //}

//        [HttpDelete("Logout")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> Logout()
//        {
//            Response.Cookies.Delete("jwt", new CookieOptions
//            {
//                HttpOnly = true,
//                SameSite = SameSiteMode.None,
//                Secure = true,
//                IsEssential = true,
//                Domain = "localhost"
//            });

//            return Ok(new
//            {
//                message = "successfully logged out"
//            });
//        }

//        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
//        {
//            var secret = _configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured");

//            var validation = new TokenValidationParameters
//            {
//                ValidIssuer = _configuration["JWT:ValidIssuer"],
//                ValidAudience = _configuration["JWT:ValidAudience"],
//                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
//                ValidateLifetime = false
//            };

//            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
//        }

//        private JwtSecurityToken GenerateJwt(string email)
//        {
//            var authClaims = new List<Claim>
//            {
//                new Claim(ClaimTypes.Email, email),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//            };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
//                _configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured")));

//            var token = new JwtSecurityToken(
//                issuer: _configuration["JWT:ValidIssuer"],
//                audience: _configuration["JWT:ValidAudience"],
//                expires: DateTime.UtcNow.AddSeconds(30),
//                claims: authClaims,
//                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
//                );

//            return token;
//        }

//        private static string GenerateRefreshToken()
//        {
//            var randomNumber = new byte[64];

//            using var generator = RandomNumberGenerator.Create();

//            generator.GetBytes(randomNumber);

//            return Convert.ToBase64String(randomNumber);
//        }
//    }
//}
