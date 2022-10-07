using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group2_BookStore.Models
{
    public class order
    {
        [DisplayName("Order ID")]
        [Key]
        public int order_id { get; set; }
        [DisplayName("Date of ID")]
        public String order_date_time { get; set; }
        [DisplayName("Customer ID")]
        public string customer_email { get; set; }
        [DisplayName("Address ID")]
        public int address_id { get; set; }
        [DisplayName("Status")]
        public int status { get; set; }
    }
}