using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkiersLab.Models;
using SkiersLab.Repository;

namespace SkiersLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Skiers1Controller : ControllerBase
    {
        private  readonly SkiersContextDB _context;
        private readonly ISkiersRep _rep;
        public  Skiers1Controller(SkiersContextDB context, ISkiersRep rep)
        {
            _rep = rep;
            _context = context;
        }

        // GET: api/Skiers1
        [HttpGet]
        public  async Task<ActionResult<IEnumerable<Skier>>> GetSkiers()
        {


            _rep.ParallelWork();
            return await _context.Skiers.ToListAsync();
        }
        
        public async Task<ActionResult<Skier>> GetSkierById(int id)
        {
            var skier = await _context.Skiers.FindAsync(id);

            if (skier == null)
            {
                return NotFound();
            }

            return skier;
        }
        // GET: api/Skiers1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Skier>> GetSkier(int id)
        {
            var skier = await _context.Skiers.FindAsync(id);

            if (skier == null)
            {
                return NotFound();
            }

            return skier;
        }

        // PUT: api/Skiers1/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkier(int id, Skier skier)
        {
            if (id != skier.fis_code)
            {
                return BadRequest();
            }

            _context.Entry(skier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkierExists(id))
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

        // POST: api/Skiers1
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Skier>> PostSkier(Skier skier)
        {
            _context.Skiers.Add(skier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkier", new { id = skier.fis_code }, skier);
        }

        // DELETE: api/Skiers1/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Skier>> DeleteSkier(int id)
        {
            var skier = await _context.Skiers.FindAsync(id);
            if (skier == null)
            {
                return NotFound();
            }

            _context.Skiers.Remove(skier);
            await _context.SaveChangesAsync();

            return skier;
        }

        private bool SkierExists(int id)
        {
            return _context.Skiers.Any(e => e.fis_code == id);
        }
    }
}
