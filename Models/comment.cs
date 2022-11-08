using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Comment
    {
        public int CommendId { get; set; }
        public string CustomerEmail { get; set; }
        public int? BookId { get; set; }
        public string ContentComment { get; set; }

        public DateTime? TimeComment { get; set; }

        public virtual Customer CustomerEmailNavigation { get; set; }
    }
}
