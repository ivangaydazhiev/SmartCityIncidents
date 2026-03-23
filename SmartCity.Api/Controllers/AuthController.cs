using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartCity.Application.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartCity.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config)    
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            if (dto.Username != "admin" || dto.Password != "1234")
            {
                return Unauthorized();
            }

            var token = GenerateToken(dto.Username);

            return Ok(new { token });
        }

        private string GenerateToken(string username)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(jwtSettings["ExpireMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
