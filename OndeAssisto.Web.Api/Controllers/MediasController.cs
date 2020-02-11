using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OndeAssisto.Common.Models;
using OndeAssisto.Web.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OndeAssisto.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MediasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Medias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Media>>> GetMedias()
        {
            return await _context.Medias.ToListAsync();
        }

        // GET: api/Medias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Media>> GetMedia(Guid id)
        {
            var media = await _context.Medias.FindAsync(id);

            if (media == null)
            {
                return NotFound();
            }

            return media;
        }

        // PUT: api/Medias/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedia(Guid id, Media media)
        {
            if (id != media.Guid)
            {
                return BadRequest();
            }

            _context.Entry(media).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Medias
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Media>> PostMedia(Media media)
        {
            _context.Medias.Add(media);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedia", new { id = media.Guid }, media);
        }

        // DELETE: api/Medias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Media>> DeleteMedia(Guid id)
        {
            var media = await _context.Medias.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }

            _context.Medias.Remove(media);
            await _context.SaveChangesAsync();

            return media;
        }

        private bool MediaExists(Guid id)
        {
            return _context.Medias.Any(e => e.Guid == id);
        }
    }
}
