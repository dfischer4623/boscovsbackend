using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoscovsBackend.Data;
using BoscovsBackend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BoscovsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BoscovsDbContext _context;
        private readonly IConfiguration _config;

        public UsersController(BoscovsDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Fixed: changed u.Email to u.email to match your model
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.email == loginDto.email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.password, user.password))
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            var token = GenerateJwtToken(user);
            return Ok(new { accessToken = token });
        }

        private string GenerateJwtToken(User user)
        {
            // Fixed Null Warning: Handled potential null for Jwt:Key
            var keyString = _config["Jwt:Key"] ?? "default_secret_key_32_characters_long";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Fixed: changed properties to lowercase (id, email, access)
            var claims = new[]
            {
                new Claim("id", user.id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.email),
                new Claim("access", user.access),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"] ?? "BoscovsBackend",
                audience: _config["Jwt:Audience"] ?? "BoscovsFrontend",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // Simple DTO for Login request
    public class LoginDto
    {
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}