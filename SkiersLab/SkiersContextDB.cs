


using Microsoft.EntityFrameworkCore;
using SkiersLab.Models;

namespace SkiersLab
{
    public class SkiersContextDB : DbContext
    {
        public SkiersContextDB(DbContextOptions<SkiersContextDB> options)
        : base(options)
        { }

        public DbSet<Skier> Skiers { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<WinterSeason> WinterSeasons { get; set; }
        public DbSet<SkiersLab.Models.RaceInfo> RaceInfo { get; set; }

    }
}
