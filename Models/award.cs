using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Award
    {

        public int Award1 { get; set; }

        public string Name { get; set; }

        public int? Year { get; set; }
    }
}
