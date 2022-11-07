using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Addresses = new List<Address>();
            Carts = new List<Cart>();
            Comments = new List<Comment>();
            Orders = new List<Order>();
            Rates = new List<Rate>();
        }
         [Required]
        public string CustomerEmail { get; set; }
         [Required]
        public string Name { get; set; }
         [Required]
        public int? Sex { get; set; }
         [Required]
        public string Image { get; set; }
         [Required]
        public DateTime? DateOfBirth { get; set; }
         [Required]
        public string PhoneNumber { get; set; }
         [Required]
        public string Password { get; set; }
         [Required]
        public int Status { get; set; }

        public virtual List<Address> Addresses { get; set; }
        public virtual List<Cart> Carts { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Rate> Rates { get; set; }
    }
}
