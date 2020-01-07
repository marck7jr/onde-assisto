namespace OndeAssisto.Common.Contracts.Jwt
{
    public interface IJwtService
    {
        public (IJwtTokenAccessData AccessToken, IJwtTokenRefreshData RefreshToken) GetJwtTokens<T>(T identity) where T : class, IJwtClaimsIdentity;
    }
}
