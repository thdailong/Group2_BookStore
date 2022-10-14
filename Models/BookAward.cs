using System;
using System.Collections.Generic;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class BookAward
    {
        public int? BookId { get; set; }
        public int? AwardId { get; set; }

        public virtual Award Award { get; set; }
        public virtual Book Book { get; set; }
    }
}
