using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public string Address1 { get; set; }
          
        public string CustomerEmail { get; set; }
          
        public string ShippingNumberPhone { get; set; }
          
        public string Name {get; set;}

        public virtual Customer CustomerEmailNavigation { get; set; }
        public virtual List<Order> Orders {get; set;}
    }
}
