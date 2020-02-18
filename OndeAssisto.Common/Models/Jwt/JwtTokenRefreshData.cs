using OndeAssisto.Common.Contracts.Jwt;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OndeAssisto.Common.Models.Jwt
{
    public class JwtTokenRefreshData : JwtToken, IJwtTokenRefreshData
    {
        [Key, JsonPropertyName("subject")]
        public Guid Guid { get; set; }
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
