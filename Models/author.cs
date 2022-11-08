using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Group2_BookStore.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new List<Book>();
        }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Detail { get; set; }
        public string Url { get; set; }

        public virtual List<Book> Books { get; set; }
    }
}
