using System;
using System.Collections.Generic;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }

        public int OrderId { get; set; }
        public DateTime? OrderDateTime { get; set; }
        public string CustomerEmail { get; set; }
        public int? AddressId { get; set; }
        public int? Status { get; set; }

        public virtual Customer CustomerEmailNavigation { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
