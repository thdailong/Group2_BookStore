using System;
using System.Collections.Generic;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Favorite
    {
        public string CustomerEmail { get; set; }
        public int BookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Customer CustomerEmailNavigation { get; set; }
    }
}
