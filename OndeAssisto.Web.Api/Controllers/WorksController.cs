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
    public class WorksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnGetWorksAsync()
        {
            return await _context.Works
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet("{guid:guid}"), AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnGetWorkAsync([FromRoute] Guid guid)
        {
            if (await _context.Works.FindAsync(guid) is Work work)
            {
                await _context.Entry(work).Reference(x => x.Author).LoadAsync();
                await _context.Entry(work).Reference(x => x.Genre).LoadAsync();

                return work;
            }

            return NotFound();
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<dynamic>> OnPostWorkAsync([FromBody] Work model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _context.Works.AnyAsync(x => x.Name == model.Name))
                {
                    return Conflict(ModelState);
                }

                if (await _context.Authors.AnyAsync(author => author.Name == model.Author.Name))
                {
                    _context.Authors.Attach(model.Author);
                }
                else
                {
                    return BadRequest(ModelState);
                }

                if (await _context.Genres.AnyAsync(genre => genre.Name == model.Genre.Name))
                {
                    _context.Genres.Attach(model.Genre);
                }
                else
                {
                    return BadRequest(ModelState);
                }

                model.CreatedAt = DateTime.UtcNow;
                model.UpdatedAt = DateTime.UtcNow;

                _context.Works.Add(model);
                await _context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, model);
            }

            return Unauthorized();
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<dynamic>> OnPutWorkAsync([FromBody] Work model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.Works.Attach(model);
                model.UpdatedAt = DateTime.UtcNow;

                _context.Works.Update(model);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return Unauthorized();
        }

        [HttpDelete, Authorize(Roles = "Administrator, Moderator")]
        public async Task<ActionResult<dynamic>> OnDeletePlatformAsync([FromBody] Work model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (await _context.Works.FindAsync(model.Guid) is Work work)
                {
                    _context.Works.Remove(work);
                    await _context.SaveChangesAsync();

                    return work;
                }

                return NotFound();
            }

            return Unauthorized();
        }
    }
}
