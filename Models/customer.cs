using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group2_BookStore.Models
{
    public class customer
    {
        [DisplayName("Customer ID")]
        [Key]
        public int customer_id { get; set; }

        [DisplayName("Full name")]
        public string name { get; set; }

        [DisplayName("Sex")]
        public int sex { get; set; }

        [DisplayName("Image URL")]
        public string image { get; set; }

        [DisplayName("Date of birth")]
        public string date_of_birth { get; set; }

        [DisplayName("Email address")]
        public string email { get; set; }

        [DisplayName("Phone number")]
        public string phone_number { get; set; }
    }
}