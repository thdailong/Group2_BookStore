using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group2_BookStore.Models
{
    public class author
    {
        [Key]
        public int author_id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string date_of_birth { get; set; }
        public string country { get; set; }
        public string detail { get; set; }
        public string url { get; set; }
    }
}