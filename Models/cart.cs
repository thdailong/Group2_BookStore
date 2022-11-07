using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Cart
    {
         [Required]
        public string CustomerEmail { get; set; }
         [Required]
        public int? BookId { get; set; }
         [Required]
        public int? Quantity { get; set; }
         [Required]
        public int CartId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Customer CustomerEmailNavigation { get; set; }
    }
}
