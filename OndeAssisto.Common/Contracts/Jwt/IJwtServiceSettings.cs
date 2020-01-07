using Microsoft.IdentityModel.Tokens;
using System;

namespace OndeAssisto.Common.Contracts.Jwt
{
    public interface IJwtServiceSettings
    {
        public string Issuer { get; }
        public string Audience { get; }
        public double AccessTokenValidForSeconds { get; }
        public double RefreshTokenValidForSeconds { get; }
        public SecurityKey SecurityKey { get; }
        public SigningCredentials SigningCredentials { get; }

        public DateTime IssuedAt { get; }
        public DateTime NotBefore { get; }
        public DateTime AccessTokenExpires { get; }
        public DateTime RefreshTokenExpires { get; }
    }
}
