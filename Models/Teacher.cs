using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Scoring.Models
{
    public class Teacher
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string registered_number { get; set; }
        [Required]
        public string image_path { get; set; }
    }
}
