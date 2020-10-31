using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkiersLab.Models
{
    public class WinterSeason
    {
        [Key]
        public int id { get; set; }
        public int year { get; set; }
        public string Kind_of_sport { get; set; }
        public int athletes_count { get; set; }

    }
}
