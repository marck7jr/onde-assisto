using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<dynamic>> OnGetAccountAsync()
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

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnPostAccountAsync([FromBody] Account model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _context.Accounts.AnyAsync(a => a.Email == model.Email))
            {
                ModelState.AddModelError("Errors", "Email is already in use.");
                return BadRequest(ModelState);
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

                _context.Entry(model).State = EntityState.Modified;
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