using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group2_BookStore.Models
{
    public class comment
    {
        [Key]
        public int commend_id { get; set; }
        public string customer_email { get; set; }
        public int book_id { get; set; }
        public string content_comment { get; set; }
        public string time_comment { get; set; }
    }
}