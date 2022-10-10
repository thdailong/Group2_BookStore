using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group2_BookStore.Models
{
    public class address
    {
        [Key]
        public int address_id { get; set; }
        public string Address { get; set; }
        public string customer_email { get; set; }
        public string shipping_number_phone { get; set; }
    }
}