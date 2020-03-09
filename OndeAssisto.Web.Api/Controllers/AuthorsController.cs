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
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnGetAuthorsAsync()
        {
            return await _context.Authors
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet("{guid:guid}"), AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnGetAuthorAsync([FromRoute] Guid guid)
        {
            if (await _context.Authors.FindAsync(guid) is Author author)
            {
                return author;
            }

            return NotFound();
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<dynamic>> OnPostAuthorAsync([FromBody] Author model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _context.Authors.AnyAsync(a => a.Name == model.Name))
                {
                    return Conflict(ModelState);
                }

                model.CreatedAt = DateTime.UtcNow;
                model.UpdatedAt = DateTime.UtcNow;

                _context.Authors.Add(model);
                await _context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, model);
            }

            return Unauthorized();
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<dynamic>> OnPutAuthorAsync([FromBody] Author model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.UpdatedAt = DateTime.UtcNow;

                _context.Authors.Update(model);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return Unauthorized();
        }

        [HttpDelete("{guid:guid}"), Authorize(Roles = "Administrator, Moderator")]
        public async Task<ActionResult<dynamic>> OnDeleteAuthorAsync([FromRoute] Guid guid)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (await _context.Authors.FindAsync(guid) is Author author)
                {
                    _context.Authors.Remove(author);
                    await _context.SaveChangesAsync();

                    return author;
                }

                return NotFound();
            }

            return Unauthorized();
        }
    }
}