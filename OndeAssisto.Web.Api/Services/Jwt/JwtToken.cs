using OndeAssisto.Common.Contracts.Jwt;
using System;
using System.Text.Json.Serialization;

namespace OndeAssisto.Web.Api.Services.Jwt
{
    public abstract class JwtToken : IJwtToken
    {
        [JsonPropertyName("created_at")]
        public DateTime IssuedAt { get; set; }
        [JsonPropertyName("expires_at")]
        public DateTime Expires { get; set; }
    }
}
