using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OndeAssisto.Common.Contracts.Jwt;
using OndeAssisto.Common.Extensions;
using OndeAssisto.Common.Models;
using OndeAssisto.Common.Models.Jwt;
using OndeAssisto.Web.Api.Data;
using System.Threading.Tasks;

namespace OndeAssisto.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwt;

        public AuthController(ApplicationDbContext context, IJwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        private async Task<JwtTokenAccessData> ProcessJwtTokensAsync<T>(T identity) where T : class, IJwtClaimsIdentity
        {
            var (AccessToken, RefreshToken) = _jwt.GetJwtTokens(identity);

            if (await _context.Tokens.FindAsync(identity.Guid) is JwtTokenRefreshData token)
            {
                _context.Tokens.Remove(token);
            }
            _context.Tokens.Add(RefreshToken as JwtTokenRefreshData);
            await _context.SaveChangesAsync();

            return AccessToken as JwtTokenAccessData;
        }

        [HttpPost("credentials"), AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnSignInWithCredentialsAsync([FromBody] JwtCredentialsIdentity model)
        {
            model.GetDerivationKeyHash(nameof(model.Password), nameof(model.Email));

            if (await _context.Accounts.FirstOrDefaultAsync(a => a.Email == model.Email && a.Password == model.Password) is Account account)
            {
                return await ProcessJwtTokensAsync(account);
            }

            return NotFound();
        }

        [HttpPost("token"), AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnSignInWithRefreshTokenAsync([FromBody] JwtTokenRefreshData model)
        {
            if (await _context.Tokens.FirstOrDefaultAsync(t => t.Guid == model.Guid && t.RefreshToken == model.RefreshToken) is JwtTokenRefreshData token)
            {
                return await ProcessJwtTokensAsync(await _context.Accounts.FindAsync(model.Guid));
            }

            return NotFound();
        }
    }
}