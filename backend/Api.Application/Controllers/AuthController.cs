using Microsoft.AspNetCore.Mvc;
using Api.Domain.Dtos; // Add this line to include the LoginDto
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Domain.Interfaces.Repository; // Add this line to include the user repository interface

namespace Api.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository; // Define the user repository

        public AuthController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository; // Initialize the user repository
        }

        [HttpPost("generate-token")]
        public async Task<IActionResult> GenerateToken([FromBody] LoginDto loginDto)
        {
            // Validate user credentials
            var user = await ValidateUserCredentials(loginDto.Email, loginDto.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }
            var userId = user.Id;
            if (userId == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Key is not configured");
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, GetUserRole(user.HierarchyLevel)),
                new Claim("email", user.Email),
                new Claim("hierarchyLevel", user.HierarchyLevel.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        private string GetUserRole(int hierarchyLevel)
        {
            return hierarchyLevel switch
            {
                0 => "Admin",
                1 => "Director",
                2 => "Manager",
                3 => "Coordinator",
                _ => "Employee" // Default role for other hierarchy levels
            };
        }

        private async Task<Api.Domain.Entities.UserEntity> ValidateUserCredentials(string email, string password)
        {
            // Get user by email
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(email, password);
            if (user == null) return null;

            // Verify user is active
            if (!user.IsActive)
            {
                return null;
            }

            // Return the authenticated user
            return user;
        }

    }
}
