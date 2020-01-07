using System;

namespace OndeAssisto.Common.Contracts.Jwt
{
    public interface IJwtClaimsIdentity
    {
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
