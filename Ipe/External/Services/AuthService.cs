
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ipe.Domain;
using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Ipe.External.Services
{
    public class JWTService : IAuthService
    {

        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration) {
            _configuration = configuration;
        }

        public string GenerateToken(User user, Roles Role)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

