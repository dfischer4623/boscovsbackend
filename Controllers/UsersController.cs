using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoscovsBackend.Data;
using BoscovsBackend.Models;
using BCrypt.Net;
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
        private readonly As400DbContext _context;
        private readonly IConfiguration _config;

        public UsersController(As400DbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            // 1. Find the user by email (matches your Sequelize logic)
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == login.Email);

            if (user == null)
            {
                return NotFound(new { message = "User Not found." });
            }

            // 2. Verify password using BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);

            if (!isPasswordValid)
            {
                return Unauthorized(new { accessToken = (string)null, message = "Invalid Password!" });
            }

            // 3. Generate JWT Token (1 hour expiry like your Node app)
            var token = GenerateJwtToken(user);

            return Ok(new
            {
                id = user.Id,
                username = user.Username,
                email = user.Email,
                access = user.Access,
                accessToken = token
            });
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("access", user.Access)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}