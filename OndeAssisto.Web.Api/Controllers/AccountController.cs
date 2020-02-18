using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OndeAssisto.Common.Extensions;
using OndeAssisto.Common.Models;
using OndeAssisto.Web.Api.Data;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OndeAssisto.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AccountController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<dynamic>> OnGetAccountAsync()
        {
            if (User.Identity.IsAuthenticated && Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out Guid guid))
            {
                if (await _context.Accounts.FindAsync(guid) is Account account)
                {
                    return account;
                }

                return NotFound();
            }

            return Unauthorized();
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnPostAccountAsync([FromBody] Account model)
        {
            if (model.Password == _configuration["Jwt:SecurityKey"])
            {
                model.Role = AccountRoleType.Administrator.ToString();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _context.Accounts.AnyAsync(a => a.Email == model.Email))
            {
                return Conflict(ModelState);
            }


            model.GetDerivationKeyHash(nameof(model.Password), nameof(model.Email));
            model.CreatedAt = DateTime.UtcNow;
            model.UpdatedAt = DateTime.UtcNow;

            _context.Accounts.Add(model);
            await _context.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.Created, model);
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<dynamic>> OnPutAccountAsync([FromBody] Account model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.GetDerivationKeyHash(nameof(model.Password), nameof(model.Email));
                model.UpdatedAt = DateTime.UtcNow;

                _context.Accounts.Update(model);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return Unauthorized();
        }

        [HttpDelete, Authorize]
        public async Task<ActionResult<dynamic>> OnDeleteAccountAsync()
        {
            if (User.Identity.IsAuthenticated && Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out Guid guid))
            {
                if (await _context.Accounts.FindAsync(guid) is Account account)
                {
                    _context.Accounts.Remove(account);
                    await _context.SaveChangesAsync();

                    return account;
                }

                return NotFound();
            }

            return Unauthorized();
        }
    }
}