using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? BookId { get; set; }
        public int? Quantity { get; set; }

        public int? Price { get; set; }


        public virtual Book Book { get; set; }
        public virtual Order Order { get; set; }
    }
}
