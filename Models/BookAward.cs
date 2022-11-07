using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class BookAward
    {
         [Required]
        public int? BookId { get; set; }
         [Required]
        public int? AwardId { get; set; }

        public virtual Award Award { get; set; }
        public virtual Book Book { get; set; }
    }
}
