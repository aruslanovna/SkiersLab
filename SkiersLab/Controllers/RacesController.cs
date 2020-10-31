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
    public class RacesController : ControllerBase
    {
        private readonly SkiersContextDB _context;

        public RacesController(SkiersContextDB context)
        {
            _context = context;
        }

        // GET: api/Races
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Race>>> GetRaces()
        {
            return await _context.Races.ToListAsync();
        }

        // GET: api/Races/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Race>> GetRace(int id)
        {
            var race = await _context.Races.FindAsync(id);

            if (race == null)
            {
                return NotFound();
            }

            return race;
        }

        // PUT: api/Races/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRace(int id, Race race)
        {
            if (id != race.codex)
            {
                return BadRequest();
            }

            _context.Entry(race).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceExists(id))
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

        // POST: api/Races
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Race>> PostRace(Race race)
        {
            _context.Races.Add(race);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRace", new { id = race.codex }, race);
        }

        // DELETE: api/Races/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Race>> DeleteRace(int id)
        {
            var race = await _context.Races.FindAsync(id);
            if (race == null)
            {
                return NotFound();
            }

            _context.Races.Remove(race);
            await _context.SaveChangesAsync();

            return race;
        }

        private bool RaceExists(int id)
        {
            return _context.Races.Any(e => e.codex == id);
        }
    }
}
