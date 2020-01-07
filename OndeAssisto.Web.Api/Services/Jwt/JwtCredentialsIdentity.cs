using OndeAssisto.Common.Contracts.Jwt;

namespace OndeAssisto.Web.Api.Services.Jwt
{
    public class JwtCredentialsIdentity : IJwtCredentialsIdentity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
