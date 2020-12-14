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

      
        public async Task<IActionResult> ParallelWork()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            ParallelLoopRace();
            AllResultsandSkiersForToday();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
             string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
             ts.Hours, ts.Minutes, ts.Seconds,
             ts.Milliseconds / 10);
             string time = "RunTime " + elapsedTime;
            return RedirectToAction("AllResultsandSkiers", new { time = time });
           
        }
        public async void ChangeAge()
        {
            var _id =await _context.Skiers.MaxAsync(x => x.fis_code);
            Skier sk = await _context.Skiers.Select(x=>x).Where(x=>x.fis_code == _id).AsNoTracking().FirstOrDefaultAsync();
            sk.age = DateTime.Now.Year - sk.year;
            _context.Skiers.Update(sk);
           await _context.SaveChangesAsync();
        }
        public async Task<IActionResult>  AllResultsandSkiers(string time)
        {
                        var query =await (from race in _context.Races
                         join result in _context.Results on race.codex equals result.raceCodex
                         join skier in _context.Skiers on result.athlete_id equals skier.fis_code
                          orderby result.time_min, result.time_sec
                         select new RaceInfo
                         {
                             country = race.country,
                             city = race.city,
                             date_race = race.date_race,
                             distance = race.distance,
                             time_min = result.time_min,
                             time_sec = result.time_sec,
                             fis_point = result.fis_point,
                             year = skier.year,
                             nation = skier.nation,
                             athlete = skier.athlete

                         })                          
                .ToListAsync();
            ViewBag.RuntimeRes = time;
            return View(query);
        }
        public async void AllResultsandSkiersForToday()
        {
               var query =  (from race in _context.Races
                               join result in _context.Results on race.codex equals result.raceCodex
                               join skier in _context.Skiers on result.athlete_id equals skier.fis_code
                               where race.date_race == DateTime.Now.ToString()
                               select new RaceInfo
                               {
                                   country = race.country,
                                   city = race.city,
                                   date_race = race.date_race,
                                   distance = race.distance,
                                   time_min = result.time_min,
                                   time_sec = result.time_sec,
                                   fis_point = result.fis_point,
                                   year = skier.year,
                                   nation = skier.nation,
                                   athlete = skier.athlete

                               }).ToListAsync();

           
        }

        public async Task ParallelLoopRace()
        {
          
            var races = await (from race in _context.Races
                                  orderby race.distance descending
                               select race ).Take(5).ToArrayAsync();
                                
            ParallelLoopResult raceParallelLoop = Parallel.ForEach<Race>(races, generateRace);
           
        }
       
        public async void generateRace(Race race)
        {
            int startRaceId =await generateRaceID();
            Race raceAdd = race;
            race.date_race = DateTime.Now.ToString();
            raceAdd.codex = ++startRaceId;
            ++startRaceId;
            _context.Races.AddAsync(raceAdd);
            _context.SaveChangesAsync();
        }

        public async Task<int> generateRaceID()
        {
            int id = await _context.Races.MaxAsync(x => x.codex);
            return id+1;
        }
        public int getResultID()
        {
            var id =  _context.Results.Max(x => x.result_id);
            return id ;
        }
        public async void generateAndAddResultforRace(int raceId)
        {
            int startResultId = getResultID();
            var skiersId = await (from sk in _context.Skiers
                                  orderby sk.year ascending
                                  select sk.fis_code).Take(200).ToArrayAsync();
            Random random = new Random();
           
            for (int i = 0; i < 200; i++) {
                Result result = new Result();
                result.result_id = ++startResultId;
                startResultId++;
                result.raceCodex = raceId;
                result.athlete_id = Convert.ToInt32(skiersId[i]);
                result.time_min = random.Next(1, 60);
                result.time_sec = random.Next(1, 60) / 10;
                result.fis_point = random.Next(0, 200);
                _context.Results.AddAsync(result);
                _context.SaveChangesAsync();
            }
        }
       
        
    }
}
