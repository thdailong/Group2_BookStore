using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
         [Required]
        public int OrderId { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}" , ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? OrderDateTime { get; set; }
         [Required]
        public string CustomerEmail { get; set; }
         [Required]
        public int? AddressId { get; set; }
         [Required]
        public int? Status { get; set; }

        public virtual Customer CustomerEmailNavigation { get; set; }
        public virtual Address Address {get; set;}
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
