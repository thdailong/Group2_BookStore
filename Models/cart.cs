using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group2_BookStore.Models
{
    public class cart
    {
        public string customer_email { get; set; }
        public int book_id { get; set; }
        public int quantity { get; set; }
        [Key]
        public int cart_id { get; set; }
    }
}