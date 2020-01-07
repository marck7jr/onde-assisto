using Microsoft.IdentityModel.Tokens;
using OndeAssisto.Common.Contracts.Jwt;
using OndeAssisto.Common.Extensions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace OndeAssisto.Web.Api.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly IJwtServiceSettings _settings;

        public JwtService(IJwtServiceSettings settings)
        {
            _settings = settings;
        }

        public (IJwtTokenAccessData AccessToken, IJwtTokenRefreshData RefreshToken) GetJwtTokens<T>(T identity) where T : class, IJwtClaimsIdentity
        {
            var claims = GetClaimsIdentity(identity);
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Audience = _settings.Audience,
                Expires = _settings.AccessTokenExpires,
                Subject = claims,
                IssuedAt = _settings.IssuedAt,
                Issuer = _settings.Issuer,
                NotBefore = _settings.NotBefore,
                SigningCredentials = _settings.SigningCredentials
            });
            var jwtAccessToken = handler.WriteToken(securityToken);
            var jwtRefreshToken = jwtAccessToken.GetDerivationKeyHash();

            return
            (
                new JwtTokenAccessData
                {
                    AccessToken = jwtAccessToken,
                    RefreshToken = jwtRefreshToken,
                    Type = "bearer",
                    IssuedAt = _settings.IssuedAt,
                    Expires = _settings.AccessTokenExpires
                },
                new JwtTokenRefreshData
                {
                    Guid = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value),
                    RefreshToken = jwtRefreshToken,
                    IssuedAt = _settings.IssuedAt,
                    Expires = _settings.RefreshTokenExpires
                }
            );
        }

        public ClaimsIdentity GetClaimsIdentity<T>(T jwtClaims) where T : class, IJwtClaimsIdentity
        {
            var hasNullValues = jwtClaims.GetType()
                .GetProperties()
                .Any(p => p.GetValue(jwtClaims) is null);

            if (hasNullValues)
            {
                throw new ArgumentNullException(nameof(jwtClaims));
            }

            return new ClaimsIdentity
            (
                new GenericIdentity(jwtClaims.Name), new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, jwtClaims.Guid.ToString()),
                    new Claim(ClaimTypes.Role, jwtClaims.Role)
                }
            );
        }
    }
}