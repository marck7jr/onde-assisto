using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OndeAssisto.Common.Contracts.Jwt;
using System;
using System.Text;

namespace OndeAssisto.Web.Api.Services.Jwt
{
    public class JwtServiceSettings : IJwtServiceSettings
    {
        private readonly IConfiguration _configuration;

        public JwtServiceSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Issuer => _configuration["Jwt:Issuer"];
        public string Audience => _configuration["Jwt:Audience"];
        public double AccessTokenValidForSeconds => double.Parse(_configuration["Jwt:Expires:Access"]);
        public double RefreshTokenValidForSeconds => double.Parse(_configuration["Jwt:Expires:Refresh"]);
        public SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
        public SigningCredentials SigningCredentials => new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

        public DateTime IssuedAt => DateTime.UtcNow;
        public DateTime NotBefore => IssuedAt;
        public DateTime AccessTokenExpires => IssuedAt.AddSeconds(AccessTokenValidForSeconds);
        public DateTime RefreshTokenExpires => IssuedAt.AddSeconds(RefreshTokenValidForSeconds);
    }
}
