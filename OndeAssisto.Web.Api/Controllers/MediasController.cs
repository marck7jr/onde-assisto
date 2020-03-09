using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OndeAssisto.Common.Models;
using OndeAssisto.Web.Api.Data;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
                .Include(x => x.Work).ThenInclude(x => x.Author)
                .Include(x => x.Work).ThenInclude(x => x.Genre)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet("account"), Authorize]
        public async Task<ActionResult<dynamic>> OnGetMediasPerAccountAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                return await _context.Medias
                    .Include(x => x.Account)
                    .Include(x => x.Work).ThenInclude(x => x.Author)
                    .Include(x => x.Work).ThenInclude(x => x.Genre)
                    .AsNoTracking()
                    .Where(x => x.Account.Guid == Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                    .ToListAsync();
            }

            return Unauthorized();
        }

        [HttpGet("{guid:guid}"), AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnGetMediaAsync([FromRoute] Guid guid)
        {
            if (await _context.Medias
                .Include(x => x.Account)
                .Include(x => x.Work).ThenInclude(x => x.Author)
                .Include(x => x.Work).ThenInclude(x => x.Genre)
                .SingleOrDefaultAsync(x => x.Guid == guid) is Media media)
            {
                media.Platforms = await _context.MediaPlatforms
                    .Include(x => x.Platform)
                    .Where(x => x.MediaGuid == guid)
                    .Select(x => new MediaPlatform
                    {
                        Platform = x.Platform
                    })
                    .ToListAsync();

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

                if (await _context.Accounts.FirstOrDefaultAsync(account => account.Guid == Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) is Account account)
                {
                    model.Account = account;
                    _context.Accounts.Attach(model.Account);
                }
                else
                {
                    return BadRequest(ModelState);
                }

                if (await _context.Works.AnyAsync(work => work.Guid == model.Work.Guid))
                {
                    _context.Works.Attach(model.Work);
                }
                else
                {
                    return BadRequest(ModelState);
                }

                foreach (var mediaPlatform in model.Platforms)
                {
                    if (await _context.Platforms.AnyAsync(x => x.Guid == mediaPlatform.Platform.Guid))
                    {
                        mediaPlatform.MediaGuid = model.Guid;
                        _context.Platforms.Attach(mediaPlatform.Platform);
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }

                _context.Medias.Add(model);
                await _context.SaveChangesAsync();
                foreach (var mediaPlatform in model.Platforms)
                {
                    mediaPlatform.Media = null;
                }

                return StatusCode((int)HttpStatusCode.Created);
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

                _context.Medias.Attach(model);
                model.UpdatedAt = DateTime.UtcNow;

                if (await _context.Accounts.FirstOrDefaultAsync(account => account.Guid == Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) is Account account)
                {
                    model.Account = account;
                    _context.Accounts.Attach(model.Account);
                }
                else
                {
                    return BadRequest(ModelState);
                }

                if (await _context.Works.AnyAsync(work => work.Guid == model.Work.Guid))
                {
                    _context.Authors.Attach(model.Work.Author);
                    _context.Genres.Attach(model.Work.Genre);
                    _context.Works.Attach(model.Work);
                }
                else
                {
                    return BadRequest(ModelState);
                }

                foreach (var mediaPlatform in model.Platforms)
                {
                    if (await _context.Platforms.AnyAsync(x => x.Guid == mediaPlatform.Platform.Guid) && await _context.MediaPlatforms.AnyAsync(x => x.MediaGuid == model.Guid))
                    {
                        mediaPlatform.MediaGuid = model.Guid;
                        mediaPlatform.PlatformGuid = mediaPlatform.Platform.Guid;
                        if (await _context.MediaPlatforms.AnyAsync(x => x.MediaGuid == mediaPlatform.MediaGuid && x.PlatformGuid == mediaPlatform.PlatformGuid))
                        {
                            _context.MediaPlatforms.Attach(mediaPlatform);
                        }
                    }
                    else
                    {
                        _context.MediaPlatforms.Add(mediaPlatform);
                    }
                }

                foreach (var mediaPlatform in await _context.MediaPlatforms.Where(x => x.MediaGuid == model.Guid).ToListAsync())
                {
                    if (!model.Platforms.Any(x => x.PlatformGuid == mediaPlatform.PlatformGuid))
                    {
                        _context.MediaPlatforms.Remove(mediaPlatform);
                    }
                }

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
