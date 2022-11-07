using System;
using System.ComponentModel.DataAnnotations;

namespace Group2_BookStore.Models
{
    public class ErrorViewModel
    {    [Required]
        public string RequestId { get; set; }
    
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}