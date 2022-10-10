using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group2_BookStore.Models
{
    public class rate
    {
        [DisplayName("Rate ID")]
        [Key]
        public int rate_id { get; set; }

        [DisplayName("Customer email")]
        public string customer_email { get; set; }

        [DisplayName("Book ID")]
        public int book_id { get; set; }

        [DisplayName("Ammount star")]
        public int amount_star { get; set; }
    }
}