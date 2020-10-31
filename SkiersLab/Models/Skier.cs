using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkiersLab.Models
{
    public class Skier
    {
        [Key]
        public int fis_code { get; set; }
        public int year { get; set; }
        public int? age { get; set; }
        public string sex { get; set; }
        public string nation { get; set; }
        public string athlete { get; set; }

    }
}
