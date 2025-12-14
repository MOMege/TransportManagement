using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Interfaces.Authentication;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration) { 
        
         _configuration = configuration;
        }
        public string GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("FullName", user.FullName ?? "")
            };

            // إضافة الـ Roles داخل الـ Token
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(jwtSettings["Key"])
               );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var expiry = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(jwtSettings["DurationInMinutes"])
            );
            var token = new JwtSecurityToken(
           issuer: jwtSettings["Issuer"],
           audience: jwtSettings["Audience"],
           claims: claims,
           expires: expiry,
           signingCredentials: creds
       );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
