using OndeAssisto.Common.Contracts.Jwt;
using System;

namespace OndeAssisto.Common.Models.Jwt
{
    public class JwtClaimsIdentity : IJwtClaimsIdentity
    {
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
