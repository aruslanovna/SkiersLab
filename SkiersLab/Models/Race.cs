using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkiersLab.Models
{
    public class Race
    {
        [Key]
        public int codex { get; set; }
     //   public string race_name { get; set; }
        public string date_race { get; set; }
        public double distance { get; set; }
         public string country{ get; set; }
        public string city { get; set; }
}
}
