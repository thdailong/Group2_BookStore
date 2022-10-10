using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group2_BookStore.Models
{
    public class book
    {
        [Key]
        public int book_id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public string image { get; set; }
        public int author_id { get; set; }
        public string publisher { get; set; }
        public string pulish_date { get; set; }
        public int page_number { get; set; }
        public string category { get; set; }
        public string size { get; set; }
        public string book_format { get; set; }
        public string detail_book { get; set; }
        public int status { get; set; }
    }
}