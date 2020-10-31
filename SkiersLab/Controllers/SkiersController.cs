using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkiersLab.Models;

namespace SkiersLab.Controllers
{
    public class SkiersController : Controller
    {
        private readonly SkiersContextDB _context;

        public SkiersController(SkiersContextDB context)
        {
            _context = context;
        }

        // GET: Skiers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skiers.ToListAsync());
        }

        // GET: Skiers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skier = await _context.Skiers
                .FirstOrDefaultAsync(m => m.fis_code == id);
            if (skier == null)
            {
                return NotFound();
            }

            return View(skier);
        }

        // GET: Skiers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skiers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("fis_code,yaer,nation,athlete")] Skier skier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skier);
        }

        // GET: Skiers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skier = await _context.Skiers.FindAsync(id);
            if (skier == null)
            {
                return NotFound();
            }
            return View(skier);
        }

        // POST: Skiers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("fis_code,yaer,nation,athlete")] Skier skier)
        {
            if (id != skier.fis_code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkierExists(skier.fis_code))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(skier);
        }

        // GET: Skiers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skier = await _context.Skiers
                .FirstOrDefaultAsync(m => m.fis_code == id);
            if (skier == null)
            {
                return NotFound();
            }

            return View(skier);
        }

        // POST: Skiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skier = await _context.Skiers.FindAsync(id);
            _context.Skiers.Remove(skier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkierExists(int id)
        {
            return _context.Skiers.Any(e => e.fis_code == id);
        }
    }
}
