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
         [Required]
        public int BookId { get; set; }
         [Required]
        public string Name { get; set; }
         [Required]
        public int? Price { get; set; }
         [Required]
        public string Image { get; set; }
         [Required]
        public int? AuthorId { get; set; }
         [Required]
        public string Publisher { get; set; }
         [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}" , ApplyFormatInEditMode = true)]
        public DateTime? PulishDate { get; set; }
         [Required]
        public int? PageNumber { get; set; }
 [Required]
        public string Category { get; set; }
         [Required]
        public string Size { get; set; }
         [Required]
        public string BookFormat { get; set; }
         [Required]
        public string DetailBook { get; set; }
         [Required]
        public int? Status { get; set; }
         [Required]
        public int? quantity {get; set;}

        public virtual Author Author { get; set; }
        public virtual List<Cart> Carts { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual List<Rate> Rates { get; set; }
    }
}
