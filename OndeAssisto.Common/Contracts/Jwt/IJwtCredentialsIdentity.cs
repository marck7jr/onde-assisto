namespace OndeAssisto.Common.Contracts.Jwt
{
    public interface IJwtCredentialsIdentity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
