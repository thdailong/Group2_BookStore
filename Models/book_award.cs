using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Group2_BookStore.Models
{

    [Keyless]
    public class book_award
    {
        public int book_id { get; set; }
        public int award_id { get; set; }
    }
}