using System;
using System.Collections.Generic;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Rate
    {
        public int RateId { get; set; }
        public string CustomerEmail { get; set; }
        public int? BookId { get; set; }
        public int? AmountStar { get; set; }

        public virtual Book Book { get; set; }
        public virtual Customer CustomerEmailNavigation { get; set; }
    }
}
