using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Book
    {
        public Book()
        {
            Carts = new List<Cart>();
            OrderDetails = new List<OrderDetail>();
            Rates = new List<Rate>();
        }
        public int BookId { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Image { get; set; }
        public int? AuthorId { get; set; }
        public string Publisher { get; set; }
        public DateTime? PulishDate { get; set; }
        public int? PageNumber { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
        public string BookFormat { get; set; }
        public string DetailBook { get; set; }
        public int? Status { get; set; }
        public int? quantity { get; set; }

        public virtual Author Author { get; set; }
        public virtual List<Cart> Carts { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual List<Rate> Rates { get; set; }
        public virtual List<Favorite> Favorites { get; set; }
    }
}
