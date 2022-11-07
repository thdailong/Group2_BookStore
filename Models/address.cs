using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Address
    {
        [Key]
        public int AddressId { get; set; }
         [Required]
        public string Address1 { get; set; }
         [Required]
        public string CustomerEmail { get; set; }
         [Required]
        public string ShippingNumberPhone { get; set; }
         [Required]
        public string Name {get; set;}

        public virtual Customer CustomerEmailNavigation { get; set; }
        public virtual List<Order> Orders {get; set;}
    }
}
