using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OndeAssisto.Common.Models;
using OndeAssisto.Web.Api.Data;
using System;
using System.Net;
using System.Threading.Tasks;

namespace OndeAssisto.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlatformsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnGetPlatformsAsync()
        {
            return await _context.Platforms
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet("{guid:guid}"), AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnGetPlatformAsync([FromRoute] Guid guid)
        {
            if (await _context.Platforms.FindAsync(guid) is Platform platform)
            {
                return platform;
            }

            return NotFound();
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<dynamic>> OnPostPlatformAsync([FromBody] Platform model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _context.Platforms.AnyAsync(x => x.Name == model.Name))
                {
                    return Conflict(ModelState);
                }

                model.CreatedAt = DateTime.UtcNow;
                model.UpdatedAt = DateTime.UtcNow;

                _context.Platforms.Add(model);
                await _context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, model);
            }

            return Unauthorized();
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<dynamic>> OnPutPlatformAsync([FromBody] Platform model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                model.UpdatedAt = DateTime.UtcNow;

                _context.Platforms.Update(model);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return Unauthorized();
        }

        [HttpDelete("{guid:guid}"), Authorize(Roles = "Administrator, Moderator")]
        public async Task<ActionResult<dynamic>> OnDeletePlatformAsync([FromRoute] Guid guid)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (await _context.Platforms.FindAsync(guid) is Platform platform)
                {
                    _context.Platforms.Remove(platform);
                    await _context.SaveChangesAsync();

                    return platform;
                }

                return NotFound();
            }

            return Unauthorized();
        }
    }
}
