

using SkiersLab.Models;
using System.Collections.Generic;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkiersLab.Repository
{
    public class SkiersRep: ISkiersRep
    {
        private readonly SkiersContextDB _context;

        public SkiersRep(SkiersContextDB context)
        {
            _context = context;
        }
        public IList<Result> res;
        public  void ParallelWork()
        {
           res =  _context.Results.Where(x=>x.raceCodex == 2105).ToList();

            Parallel.ForEach(res, r =>
             {
                 int id = r.raceCodex;
                 int id2 = r.result_id;
                 Thread.Sleep(3000);
             });
            AllResultsandSkiers();
            //    (new List<int>() { 1048473 },  ChangeAge);
            //Parallel.ForEach<int>(new List<int>() { 2105 },
            //   ChangeRaceCountry);
        }
        public async void ChangeAge(int skierId)
        {
            Skier sk =await _context.Skiers.FindAsync(skierId);
            sk.age = sk.age + 1;
            Thread.Sleep(3000);
        }
        public RaceInfo AllResultsandSkiers()
        {

            var query = (from race in _context.Races
                        join result in _context.Results on race.codex equals result.raceCodex
                        join skier in _context.Skiers on result.athlete_id equals skier.fis_code
                        select new RaceInfo
                        {
                            country = race.country,
                            city = race.city,
                            date_race = race.date_race,
                            distance = race.distance,
        
        time_min = result.time_min,
        time_sec =result.time_sec,
       fis_point =result.fis_point,
       year = skier.year,

             nation = skier.nation,
         athlete = skier.athlete

    }).FirstOrDefault();

            return (RaceInfo)query;

        }
       
        public async void ChangeRaceCountry(int RaceId)
        {
            Race sk =await _context.Races.FindAsync(RaceId);
            sk.country = "Changed";
        }
    }
}
