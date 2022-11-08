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
        public string CustomerEmail { get; set; }
        public string Name { get; set; }
        public int? Sex { get; set; }
        public string Image { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public int Status { get; set; }

        public virtual List<Address> Addresses { get; set; }
        public virtual List<Cart> Carts { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Rate> Rates { get; set; }
        public virtual List<Favorite> Favorites { get; set; }
    }
}
