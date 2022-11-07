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
         [Required]
        public int AuthorId { get; set; }
         [Required]
        public string Name { get; set; }
         [Required]
        public byte[] Image { get; set; }
         [Required]
        public DateTime? DateOfBirth { get; set; }
         [Required]
        public string Country { get; set; }
         [Required]
        public string Detail { get; set; }
         [Required]
        public string Url { get; set; }

        public virtual List<Book> Books { get; set; }
    }
}
