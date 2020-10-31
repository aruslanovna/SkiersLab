using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiersLab;
using SkiersLab.Models;

namespace SkiersLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinterSeasonsController : ControllerBase
    {
        private readonly SkiersContextDB _context;

        public WinterSeasonsController(SkiersContextDB context)
        {
            _context = context;
        }

        // GET: api/WinterSeasons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WinterSeason>>> GetWinterSeasons()
        {
            return await _context.WinterSeasons.ToListAsync();
        }

        // GET: api/WinterSeasons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WinterSeason>> GetWinterSeason(int id)
        {
            var winterSeason = await _context.WinterSeasons.FindAsync(id);

            if (winterSeason == null)
            {
                return NotFound();
            }

            return winterSeason;
        }

        // PUT: api/WinterSeasons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWinterSeason(int id, WinterSeason winterSeason)
        {
            if (id != winterSeason.id)
            {
                return BadRequest();
            }

            _context.Entry(winterSeason).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WinterSeasonExists(id))
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

        // POST: api/WinterSeasons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WinterSeason>> PostWinterSeason(WinterSeason winterSeason)
        {
            _context.WinterSeasons.Add(winterSeason);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWinterSeason", new { id = winterSeason.id }, winterSeason);
        }

        // DELETE: api/WinterSeasons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WinterSeason>> DeleteWinterSeason(int id)
        {
            var winterSeason = await _context.WinterSeasons.FindAsync(id);
            if (winterSeason == null)
            {
                return NotFound();
            }

            _context.WinterSeasons.Remove(winterSeason);
            await _context.SaveChangesAsync();

            return winterSeason;
        }

        private bool WinterSeasonExists(int id)
        {
            return _context.WinterSeasons.Any(e => e.id == id);
        }
    }
}
