using System;

namespace OndeAssisto.Common.Contracts.Jwt
{
    public interface IJwtTokenRefreshData : IJwtToken
    {
        public Guid Guid { get; set; }
        public string RefreshToken { get; set; }
    }
}
