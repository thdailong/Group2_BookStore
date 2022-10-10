using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group2_BookStore.Models
{
    public class order_detail
    {
        [DisplayName("Order detail ID")]
        [Key]
        public int order_detail_id { get; set; }
        [DisplayName("Order ID")]
        public int order_id { get; set; }
        [DisplayName("Book ID")]
        public int book_id { get; set; }
        [DisplayName("Quantity")]
        public int quantity { get; set; }
        [DisplayName("Price")]
        public float price { get; set; }
    }
}