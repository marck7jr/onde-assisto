using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OndeAssisto.Common.Models;
using OndeAssisto.Web.Api.Data;

namespace OndeAssisto.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private ApplicationDbContext _context;

        public PlatformsController(ApplicationDbContext context)
        {
            _context = context;

        }
    
       [HttpGet] public dynamic OnGet(Guid guid)
        {
            if (_context.Platforms.FirstOrDefault(platform => platform.Guid == guid) is Platform platform)
                return platform;
            else
                return NotFound();
        }

        [HttpPost]public async Task<dynamic>  OnPost(Platform platform)
        {
            if (ModelState.IsValid)
            {
                _context.Platforms.Add(platform);
                await _context.SaveChangesAsync();

                return Ok(platform);
            }
            else
                return BadRequest(ModelState);

        }


           
    
    
    }
}