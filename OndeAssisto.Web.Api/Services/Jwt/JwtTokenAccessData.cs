using OndeAssisto.Common.Contracts.Jwt;
using System.Text.Json.Serialization;

namespace OndeAssisto.Web.Api.Services.Jwt
{
    public class JwtTokenAccessData : JwtToken, IJwtTokenAccessData
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("token_type")]
        public string Type { get; set; }
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
