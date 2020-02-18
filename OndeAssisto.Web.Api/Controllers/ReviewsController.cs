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
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnGetReviewsAsync()
        {
            return await _context.Reviews
                .Include(x => x.Account)
                .Include(x => x.Media)
                .ToListAsync();
        }

        [HttpGet("{guid:guid}"), AllowAnonymous]
        public async Task<ActionResult<dynamic>> OnGetReviewAsync([FromRoute] Guid guid)
        {
            if (await _context.Reviews.FindAsync(guid) is Review review)
            {
                return review;
            }

            return NotFound();
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<dynamic>> OnPostReviewAsync([FromBody] Review model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _context.Reviews.AnyAsync(x => x.Account.Guid == model.Account.Guid))
                {
                    return Conflict(ModelState);
                }

                model.Account = await _context.Accounts.FindAsync(model.Account.Guid);
                model.CreatedAt = DateTime.UtcNow;
                model.UpdatedAt = DateTime.UtcNow;

                _context.Reviews.Add(model);
                await _context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, model);
            }

            return Unauthorized();
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<dynamic>> OnPutMediaAsync([FromBody] Review model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                model.UpdatedAt = DateTime.UtcNow;

                _context.Reviews.Update(model);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return Unauthorized();
        }

        [HttpDelete, Authorize]
        public async Task<ActionResult<dynamic>> OnDeleteReviewAsync([FromBody] Review model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (await _context.Reviews.FindAsync(model.Guid) is Review review)
                {
                    _context.Reviews.Remove(review);
                    await _context.SaveChangesAsync();

                    return review;
                }

                return NotFound();
            }

            return Unauthorized();
        }
    }
}
