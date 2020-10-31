using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace SkiersLab.Models
{
    public class Result
    {
        [Key]
        public int result_id { get; set; }
        public int raceCodex { get; set; }
        public int athlete_id { get; set; }
        public int time_min { get; set; }
        public double time_sec { get; set; }
        public double fis_point { get; set; }

    }
}
