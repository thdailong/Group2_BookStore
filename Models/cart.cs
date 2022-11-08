using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Cart
    {
        public string CustomerEmail { get; set; }
        public int? BookId { get; set; }
        public int? Quantity { get; set; }
        public int CartId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Customer CustomerEmailNavigation { get; set; }
    }
}
