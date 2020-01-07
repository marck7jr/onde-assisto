using System;

namespace OndeAssisto.Common.Contracts.Jwt
{
    public interface IJwtToken
    {
        public DateTime IssuedAt { get; set; }
        public DateTime Expires { get; set; }
    }
}
