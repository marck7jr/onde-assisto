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
    public class MediasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MediasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnGetMediasAsync()
        {
            return await _context.Medias
                .Include(x => x.Account)
                .Include(x => x.Platforms)
                .Include(x => x.Work)
                .ToListAsync();
        }

        [HttpGet("{guid:guid}"), AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnGetMediaAsync([FromRoute] Guid guid)
        {
            if (await _context.Medias.FindAsync(guid) is Media media)
            {
                await _context.Entry(media).Reference(x => x.Account).LoadAsync();
                await _context.Entry(media).Reference(x => x.Work).LoadAsync();
                await _context.Entry(media.Work).Reference(x => x.Author).LoadAsync();
                await _context.Entry(media).Collection(x => x.Platforms).LoadAsync();

                return media;
            }

            return NotFound();
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<dynamic>> OnPostMediaAsync([FromBody] Media model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _context.Medias.AnyAsync(x => x.Work.Guid == model.Work.Guid))
                {
                    return Conflict(ModelState);
                }

                model.CreatedAt = DateTime.UtcNow;
                model.UpdatedAt = DateTime.UtcNow;

                model.Account = await _context.Accounts.FindAsync(model.Account.Guid);
                model.Work = await _context.Works.FindAsync(model.Work.Guid);
                model.Platforms.ForEach(async x => x = await _context.Platforms.FindAsync(x.Guid));
                _context.Medias.Add(model);
                await _context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, model);
            }

            return Unauthorized();
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<dynamic>> OnPutMediaAsync([FromBody] Media model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.UpdatedAt = DateTime.UtcNow;
                _context.Medias.Update(model);

                await _context.SaveChangesAsync();

                return NoContent();
            }

            return Unauthorized();
        }

        [HttpDelete("{guid:guid}"), Authorize(Roles = "Administrator, Moderator")]
        public async Task<ActionResult<dynamic>> OnDeleteMediaAsync([FromRoute] Guid guid)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (await _context.Medias.FindAsync(guid) is Media media)
                {
                    _context.Medias.Remove(media);
                    await _context.SaveChangesAsync();

                    return media;
                }
            }

            return Unauthorized();
        }
    }
}
