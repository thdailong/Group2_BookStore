using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Comment
    {
         [Required]
        public int CommendId { get; set; }
         [Required]
        public string CustomerEmail { get; set; }
         [Required]
        public int? BookId { get; set; }
         [Required]
        public string ContentComment { get; set; }
         [Required]
        public DateTime? TimeComment { get; set; }

        public virtual Customer CustomerEmailNavigation { get; set; }
    }
}
