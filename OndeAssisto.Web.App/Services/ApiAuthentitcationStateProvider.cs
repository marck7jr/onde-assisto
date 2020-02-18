using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using OndeAssisto.Common.Models.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OndeAssisto.Web.App.Services
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storage;

        public ApiAuthenticationStateProvider(ILocalStorageService storage)
        {
            _storage = storage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _storage.GetItemAsync<string>(nameof(JwtTokenAccessData.AccessToken)) is string token)
            {
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var identity = new ClaimsIdentity("Bearer");
                foreach (var claim in jwtToken.Claims)
                {
                    switch (claim.Type)
                    {
                        case "unique_name":
                            {
                                identity.AddClaim(new Claim(ClaimTypes.Name, claim.Value));
                                break;
                            }
                        case "role":
                            {
                                identity.AddClaim(new Claim(ClaimTypes.Role, claim.Value));
                                break;
                            }
                        case "nameid":
                            {
                                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, claim.Value));
                                break;
                            }
                        default:
                            {
                                identity.AddClaim(claim);
                                break;
                            }
                    }
                }
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
}
