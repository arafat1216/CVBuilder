using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Models.Authentication;
using CVBuilder.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CVBuilder.Infrastructure.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        

        public TokenGeneratorService(IOptions<JwtSettings> jwtSettings)
        {
            JwtSettings = jwtSettings.Value;
        }

        public JwtSettings JwtSettings { get; }

        public string GenerateToken(Employee employee)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, employee.EmployeeId.ToString()),
                new Claim(ClaimTypes.Email, employee.Email),
                new Claim(ClaimTypes.Role, employee.Role.ToString())
            };

            var securityToken = new JwtSecurityToken(
                JwtSettings.Issuer,
                JwtSettings.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials

                );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
