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
        [Required(ErrorMessage = "Email can not be null")]
        public string CustomerEmail { get; set; }
        [Required(ErrorMessage = "Name can not be null")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Sex can not be null")]
        public int? Sex { get; set; }

        public string Image { get; set; }
        [Required(ErrorMessage = "Date of birth can not be null")]
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessage = "Phone number can not be null")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password can not be null")]

        public string Password { get; set; }
        [Required(ErrorMessage = "Status can not be null")]
        public int Status { get; set; }

        public virtual List<Address> Addresses { get; set; }
        public virtual List<Cart> Carts { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Rate> Rates { get; set; }
        public virtual List<Favorite> Favorites { get; set; }
    }
}
