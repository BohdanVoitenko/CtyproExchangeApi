using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Application.UsersAuth.Commands.CreateUserCommand;
using CryptoExchange.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CryptoExchange.Application.Common.JwtAuthentication
{
    public class JwtAuthService : IAuthService
    {
        private readonly JwtSettings _settings;

        public JwtAuthService(IConfiguration configuration)
        {
            _settings = new JwtSettings();
            configuration.Bind("JwtSettings", _settings);
        }

        public string GenerateJwtToken(AppUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_settings.Issuer,
                _settings.Issuer,
                new List<Claim>
                {
                    new (ClaimTypes.Name, user.UserName),
                    new (ClaimTypes.Email, user.Email),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                },
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}

