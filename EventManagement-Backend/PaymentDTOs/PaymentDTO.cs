using System.ComponentModel.DataAnnotations;

namespace EventManagement_Backend.PaymentDTOs
{
    public class PaymentDTO
    {
        [Key]
        public int PaymentID { get; set; }
        public string UserId { get; set; }
        //public int EventID { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}
