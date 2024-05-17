using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Scoring.Models
{
    public class Subject
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string level { get; set; }
        [Required]
        public string semester { get; set; }
        [Required]
        public string major { get; set; }
        [Required]
        public string description { get; set; }
    }
}
