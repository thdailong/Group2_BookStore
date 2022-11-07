using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class OrderDetail
    {    [Required]
        public int OrderDetailId { get; set; }
         [Required]
        public int? OrderId { get; set; }
         [Required]
        public int? BookId { get; set; }
         [Required]
        public int? Quantity { get; set; }
         [Required]
        public int? Price { get; set; }
        

        public virtual Book Book { get; set; }
        public virtual Order Order { get; set; }
    }
}
