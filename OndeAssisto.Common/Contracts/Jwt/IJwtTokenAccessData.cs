namespace OndeAssisto.Common.Contracts.Jwt
{
    public interface IJwtTokenAccessData : IJwtToken
    {
        public string AccessToken { get; set; }
        public string Type { get; set; }
        public string RefreshToken { get; set; }
    }
}
