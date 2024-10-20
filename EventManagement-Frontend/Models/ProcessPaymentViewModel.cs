using Microsoft.CodeAnalysis.Completion;
using System.ComponentModel.DataAnnotations;

namespace EventManagement_Frontend.Models
{
    public class ProcessPaymentViewModel
    {
        public string UserId { get; set; }
        public int EventId { get; set; }
        public decimal TotalAmount { get; set; }
        [Required]
        public string CVV { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Enter the expery date")]
        public string ExpirationDate { get; set; } // in "yyyy-MM-dd" format
    }
}
