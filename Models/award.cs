using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group2_BookStore.Models
{
    public class award
    {
        [Key]
        public int Award { get; set; }
        public string name { get; set; }
        public int year { get; set; }
    }
}