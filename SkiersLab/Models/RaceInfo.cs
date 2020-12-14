using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkiersLab.Models
{
    public class RaceInfo
    {
        [Key]
        public int codex { get; set; }
        //   public string race_name { get; set; }
        public string date_race { get; set; }
        public double distance { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public int time_min { get; set; }
        public double time_sec { get; set; }
        public double fis_point { get; set; }
        public int year { get; set; }
              public string nation { get; set; }
        public string athlete { get; set; }
    }
}
