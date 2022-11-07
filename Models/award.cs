using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Award
    {
         [Required]
        public int Award1 { get; set; }
         [Required]
        public string Name { get; set; }
         [Required]
        public int? Year { get; set; }
    }
}
