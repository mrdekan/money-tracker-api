using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using money_tracker.Application.Interfaces;
using money_tracker.Domain.Entities;

namespace money_tracker.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly ConfigManager _configuration;

        public JwtService(ConfigManager configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration.JwtIssuer,
                _configuration.JwtAudience,
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
