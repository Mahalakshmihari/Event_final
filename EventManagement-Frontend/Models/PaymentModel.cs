namespace EventManagement_Frontend.Models
{
    public class PaymentModel
    {
        public int PaymentId { get; set; }
        public string UserId { get; set; } = null!;
        public int EventId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; } = null!;
        public int Cvv { get; set; }
        public long CardNumber { get; set; }
        public DateOnly ExpiryDate { get; set; }
    }
}
