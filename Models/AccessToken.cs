using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Scoring.Models
{
    public class AccessToken
    {
        public int id { get; set; }
        [Required]
        public string token { get; set; }
        [Required]
        public DateTime expired_at { get; set; }
        [Required]
        public DateTime created_at { get; set; }
    }
}
